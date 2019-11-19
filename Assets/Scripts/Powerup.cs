using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    Small,
    Extended,
    Magnet,
    Laser,
    FireBall,
    BombBall,
    SplitBall,
    GiantBall
}

public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PaddleManager.Instance.currentPaddle)
        {
            //GameObject levelManager = GameObject.Find("Level Manager");
            //PaddleManager paddleManager = levelManager.GetComponent<PaddleManager>();
            //BallManager ballManager = levelManager.GetComponent<BallManager>();

            switch (powerupType)
            {
                case PowerupType.Small:
                    PaddleManager.Instance.ChangePaddle(PaddleType.Small);
                    break;

                case PowerupType.Extended:
                    PaddleManager.Instance.ChangePaddle(PaddleType.Extended);
                    break;

                case PowerupType.Magnet:
                    PaddleManager.Instance.ChangePaddle(PaddleType.Magnet);
                    break;

                case PowerupType.Laser:
                    PaddleManager.Instance.ChangePaddle(PaddleType.Laser);
                    break;

                case PowerupType.FireBall:
                    BallManager.Instance.ChangeAllBalls(BallType.Fire);
                    break;

                case PowerupType.GiantBall:
                    BallManager.Instance.ChangeAllBalls(BallType.Giant);
                    break;

                case PowerupType.BombBall:
                    BallManager.Instance.ChangeAllBalls(BallType.Bomb);
                    break;

                case PowerupType.SplitBall:
                    BallManager.Instance.SplitAllBalls();
                    break;
            }

            Destroy(gameObject);
        }
    }
}