using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseArea : MonoBehaviour
{
    public LevelManager levelManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = BallManager.Instance.Balls.Find(b => b.GameObject == collision.gameObject);
        if (ball != null)
        {
            if (BallManager.Instance.Balls.Count > 1)
            {
                BallManager.Instance.DestroyBall(ball);
                // TODO: Add particle effect
            }
            else
            {
                if (PaddleManager.Instance.extraLives > 0)
                {
                    PaddleManager.Instance.LosePaddle();
                    levelManager.ResetPaddleAndBall();
                }
                else
                {
                    PaddleManager.Instance.GainPaddle(2);
                    levelManager.ResetGame();
                }
            }
        }
    }
}