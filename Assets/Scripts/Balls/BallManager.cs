using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    Normal,
    Fire,
    Bomb,
    Giant
}

public class BallManager : Singleton<BallManager>
{
    public GameObject normal;
    public GameObject fire;
    public GameObject bomb;
    public GameObject giant;

    public List<Ball> Balls { get; } = new List<Ball>();
    public GameObject startupBall;
    public BallType startupBallType;

    private void Start()
    {
        Ball newBall = new Ball(startupBall, startupBallType);
        Balls.Add(newBall);
    }

    public Ball CreateBall(BallType type, Vector3 position)
    {
        // Instantiate new ball
        GameObject newBallObject = Instantiate(GetPrefab(type), position, Quaternion.identity);
        Ball newBall = new Ball(newBallObject, type);

        // Add it to the list
        Balls.Add(newBall);

        return newBall;
    }

    public void DestroyBall(Ball ball)
    {
        Balls.Remove(ball);
        Destroy(ball.GameObject);
    }

    public Ball ChangeBall(Ball currentBall, BallType newBallType)
    {
        // Instantiate new ball object
        GameObject newBallObject = Instantiate(GetPrefab(newBallType), currentBall.GameObject.transform.position, Quaternion.identity);
        Ball newBall = new Ball(newBallObject, newBallType);

        newBall.RigidBody2D.velocity = currentBall.RigidBody2D.velocity;
        newBall.BallMovement.attachedToPaddle = currentBall.BallMovement.attachedToPaddle;

        // Replace the reference on the list with the new one
        //Balls[Balls.FindIndex(ball => ball.GameObject == currentBall.GameObject)] = newBall;
        Balls[Balls.IndexOf(currentBall)] = newBall;
        Destroy(currentBall.GameObject);

        return newBall;
    }

    public void ChangeAllBalls(BallType newBallType)
    {
        for (int index = 0; index < Balls.Count; index++)
            ChangeBall(Balls[index], newBallType);
    }

    public void SplitAllBalls()
    {
        // Iterate all the balls onscreen
        int initialBallCount = Balls.Count;
        for (int index = 0; index < initialBallCount; index++)
        {
            // Detach the ball if necessary
            if (Balls[index].BallMovement.attachedToPaddle)
                Balls[index].BallMovement.DetachFromPaddle();

            // Delay split until the ball is confirmed to be dettached and moving
            StartCoroutine(SplitBall(Balls[index]));
        }
    }

    private IEnumerator SplitBall(Ball ball)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            // Wait for the ball to be moving before splitting
            if (ball.RigidBody2D.velocity.magnitude > 0)
            {
                // Create a new ball in the same position as the current one
                Ball newBall = CreateBall(ball.Type, ball.RigidBody2D.position);
                newBall.BallMovement.attachedToPaddle = false;
                newBall.RigidBody2D.velocity = ball.RigidBody2D.velocity;

                ball.RigidBody2D.velocity = Quaternion.Euler(0, 0, 15) * ball.RigidBody2D.velocity;
                newBall.RigidBody2D.velocity = Quaternion.Euler(0, 0, -15) * newBall.RigidBody2D.velocity;

                yield break;
            }
        }
    }

    private GameObject GetPrefab(BallType type)
    {
        switch (type)
        {
            case BallType.Normal: return normal;
            case BallType.Fire: return fire;
            case BallType.Bomb: return bomb;
            case BallType.Giant: return giant;
            default: return null;
        }
    }
}