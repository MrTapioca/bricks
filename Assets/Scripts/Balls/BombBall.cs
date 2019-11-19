using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBall : MonoBehaviour
{
    public GameObject explosion;

    //private BallManager ballManager;
    //private Ball thisBall;

    private void Start()
    {
        //ballManager = GameObject.Find("Level Manager").GetComponent<BallManager>();
        //thisBall = ballManager.Balls.Find(ball => ball.GameObject == gameObject);
        //thisBall = BallManager.Instance.Balls.Find(ball => ball.GameObject == gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            Ball thisBall = BallManager.Instance.Balls.Find(ball => ball.GameObject == gameObject);
            if (thisBall != null)
            {
                // Create explosion
                Instantiate(explosion, transform.position, Quaternion.identity);

                // Change bomb back to normal ball
                //ballManager.ChangeBall(thisBall, BallType.Normal);
                BallManager.Instance.ChangeBall(thisBall, BallType.Normal);
            }
        }
    }
}