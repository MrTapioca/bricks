using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    //public PaddleManager paddleManager;
    public float paddleSpeed;

    private Camera mainCamera;
    private float newX;
    private bool incremental;
    private bool dragging;

    private bool initialLaunchDone;

    private GameObject rightWall;
    private GameObject leftWall;

    private void Awake()
    {
        mainCamera = Camera.main;

        // Get wall instances to allow calculations
        rightWall = GameObject.Find("Right Wall");
        leftWall = GameObject.Find("Left Wall");
    }

    private void FixedUpdate()
    {
        // Check if the paddle needs to move
        if (dragging)
        {
            Vector2 paddlePosition = PaddleManager.Instance.currentPaddleBody.position;
            if ((incremental && paddlePosition.x < newX) || (!incremental && paddlePosition.x > newX))
            {
                // Decide the distance to move, whether a full speed step or less
                float distance = paddleSpeed;
                if (Mathf.Abs(newX - paddlePosition.x) < paddleSpeed)
                    distance = Mathf.Abs(newX - paddlePosition.x);

                // Make sure the paddle doesn't go past the walls if it's holding balls
                float ballsMaxRight = 0;
                float ballsMaxLeft = 0;

                foreach (Ball ball in BallManager.Instance.Balls)
                {
                    if (ball.BallMovement.attachedToPaddle)
                    {
                        var ballPosition = ball.GameObject.transform.position;
                        var ballSize = ball.CircleCollider2D.radius;

                        var ballRight = ballPosition.x + ballSize;
                        var ballLeft = ballPosition.x - ballSize;

                        if (ballRight > ballsMaxRight)
                            ballsMaxRight = ballRight;
                        if (ballLeft < ballsMaxLeft)
                            ballsMaxLeft = ballLeft;
                    }
                }

                if (incremental && ballsMaxRight > 0)
                {
                    if (ballsMaxRight + distance > rightWall.transform.position.x)
                        distance -= (ballsMaxRight + distance) - rightWall.transform.position.x;
                }

                if (!incremental && ballsMaxLeft < 0)
                {
                    if (ballsMaxLeft - distance < leftWall.transform.position.x)
                        distance += (ballsMaxLeft - distance) - leftWall.transform.position.x;
                }

                Vector2 newPosition = new Vector2(paddlePosition.x + (incremental ? distance : -distance), paddlePosition.y);
                //paddleManager.currentPaddleBody.position = newPosition;
                PaddleManager.Instance.currentPaddleBody.MovePosition(newPosition);
            }
        }
    }

    public void CancelDrag()
    {
        dragging = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            HandleTouch(Input.GetTouch(0));
        }
    }

    private void HandleTouch(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == name)
                {
                    dragging = true;
                    UpdateNewX(touch.position);
                }
                break;

            case TouchPhase.Moved when dragging:
                UpdateNewX(touch.position);
                break;

            case TouchPhase.Ended when dragging:
                dragging = false;

                // Release all the attached balls
                foreach (Ball ball in BallManager.Instance.Balls)
                {
                    if (ball.BallMovement.attachedToPaddle)
                        ball.BallMovement.DetachFromPaddle();
                }

                if (!initialLaunchDone)
                {
                    TimeManager.Instance.StartTimer();
                    initialLaunchDone = true;
                }

                break;

            case TouchPhase.Canceled when dragging:
                dragging = false;
                break;
        }
    }

#if UNITY_EDITOR
    private void OnMouseDown()
    {
        HandleTouch(new Touch { position = Input.mousePosition, phase = TouchPhase.Began });
    }

    private void OnMouseDrag()
    {
        HandleTouch(new Touch { position = Input.mousePosition, phase = TouchPhase.Moved });
    }

    private void OnMouseUp()
    {
        HandleTouch(new Touch { position = Input.mousePosition, phase = TouchPhase.Ended });
    }
#endif

    private void UpdateNewX(Vector2 pixelPosition)
    {
        // Save location and direction of new position
        newX = mainCamera.ScreenToWorldPoint(pixelPosition).x;
        incremental = PaddleManager.Instance.currentPaddleBody.position.x <= newX;
    }
}