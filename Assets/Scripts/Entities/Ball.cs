using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball
{
    public GameObject GameObject { get; }
    public BallType Type { get; }
    public Rigidbody2D RigidBody2D { get; }
    public CircleCollider2D CircleCollider2D { get; }
    public BallMovement BallMovement { get; }

    public Ball(GameObject ballGameObject, BallType ballType)
    {
        GameObject = ballGameObject;
        Type = ballType;
        RigidBody2D = ballGameObject.GetComponent<Rigidbody2D>();
        CircleCollider2D = ballGameObject.GetComponent<CircleCollider2D>();
        BallMovement = ballGameObject.GetComponent<BallMovement>();
    }
}