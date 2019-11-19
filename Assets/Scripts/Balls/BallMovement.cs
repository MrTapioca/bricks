using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed;
    public bool attachedToPaddle;

    private bool detachingFromPaddle;
    public bool AttachedByMagnet { get; private set; }

    private GameObject rightWall;
    private GameObject leftWall;

    private Rigidbody2D ballBody;
    private CircleCollider2D ballCollider;
    private GameObject trail;

    private int hitMultiplier = 1;

    private float maxPaddleBounceAngle = 75 * Mathf.Deg2Rad;

    private void Awake()
    {
        // Get wall instances to allow calculations
        rightWall = GameObject.Find("Right Wall");
        leftWall = GameObject.Find("Left Wall");

        // Get ball components
        ballBody = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<CircleCollider2D>();
        trail = transform.Find("Trail").gameObject;
    }

    private void Start()
    {
        if (attachedToPaddle)
            // This ball was created attached to paddle
            AttachToPaddle();
    }

    private void OnEnable()
    {
        PaddleManager.OnPaddleChanging += PaddleManager_OnPaddleChanging;
    }

    private void OnDisable()
    {
        PaddleManager.OnPaddleChanging -= PaddleManager_OnPaddleChanging;
    }

    private void PaddleManager_OnPaddleChanging(PaddleType newType, GameObject newPaddle, Rigidbody2D newPaddleBody)
    {
        if (attachedToPaddle)
        {
            if (PaddleManager.Instance.currentType == PaddleType.Magnet && newType != PaddleType.Magnet && AttachedByMagnet)
                DetachFromPaddle();
            else
                // Change parent to new paddle
                transform.SetParent(newPaddle.transform);
        }
    }

    public void AttachToPaddle()
    {
        // Attach to paddle as child
        transform.SetParent(PaddleManager.Instance.currentPaddle.transform);

        // Disable ball phisics and trail
        ballBody.bodyType = RigidbodyType2D.Kinematic;
        ballBody.velocity = Vector2.zero;
        trail.SetActive(false);

        // Make sure the ball touches right above the paddle collider
        var paddlePosition = PaddleManager.Instance.currentPaddleBody.position;
        var paddleSize = PaddleManager.Instance.currentPaddleCollider.size;
        var paddleScale = PaddleManager.Instance.currentPaddle.transform.lossyScale;

        var paddleColliderTop = paddlePosition.y + ((paddleSize.y / 2) * paddleScale.y);
        var newPositionY = paddleColliderTop + (ballCollider.radius * transform.lossyScale.y);
        transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);

        attachedToPaddle = true;
    }

    public void DetachFromPaddle()
    {
        // Remove from paddle parent
        transform.SetParent(null);

        // Enable ball physics and trail
        ballBody.bodyType = RigidbodyType2D.Dynamic;
        trail.SetActive(true);

        attachedToPaddle = false;
        detachingFromPaddle = true;

        AttachedByMagnet = false;
    }

    private void LateUpdate()
    {
        // Mantain constant velocity
        if (!attachedToPaddle)
            ballBody.velocity = speed * (ballBody.velocity.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attachedToPaddle)
        {
            if (collision.gameObject == PaddleManager.Instance.currentPaddle)
            {
                // Check if ball needs to attach to magnet paddle
                if (!detachingFromPaddle && PaddleManager.Instance.currentType == PaddleType.Magnet)
                {
                    AttachedByMagnet = true;
                    AttachToPaddle();
                }
                // Calculate paddle bounce
                else
                {
                    float paddleX = collision.collider.transform.position.x;
                    float paddleWidth = collision.collider.bounds.size.x;
                    float intersectX = collision.GetContact(0).point.x;

                    // Get X position of ball intersection relative to the paddle
                    float relativeIntersectX = (intersectX - paddleX);
                    // Normalize the X position; make it fall between -1 and 1
                    float normalizedRelativeIntersectionX = (relativeIntersectX / (paddleWidth / 2));
                    // Calculate the bounce angle using the normalized X and the max bounce angle in radians
                    float bounceAngle = normalizedRelativeIntersectionX * maxPaddleBounceAngle;

                    // Calculate the direction using the appropriate X and Y ratio
                    Vector2 newDirection = new Vector2(Mathf.Sin(bounceAngle), Mathf.Cos(bounceAngle));
                    ballBody.velocity = speed * newDirection;

                    hitMultiplier = 1;
                }
            }
            else if (collision.gameObject.CompareTag("Block"))
            {
                ScoreManager.Instance.AddPoints(ScoreAction.BrickHit, hitMultiplier);
                hitMultiplier++;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        detachingFromPaddle = false;
    }
}