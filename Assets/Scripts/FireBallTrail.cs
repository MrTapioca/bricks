using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrail : MonoBehaviour
{
    private Rigidbody2D ballBody;

    private void Awake()
    {
        ballBody = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Rotates the trail based on the direction of the ball
        transform.rotation = Quaternion.Euler(0, 0, -(Mathf.Atan2(ballBody.velocity.x, ballBody.velocity.y) * Mathf.Rad2Deg));
    }
}