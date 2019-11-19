using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimeout : MonoBehaviour
{
    public float timeout = 10;

    //private BallManager ballManager;
    private Ball thisBall;

    void Start()
    {
        //ballManager = GameObject.Find("Level Manager").GetComponent<BallManager>();
        //thisBall = ballManager.Balls.Find(ball => ball.GameObject == gameObject);
        thisBall = BallManager.Instance.Balls.Find(ball => ball.GameObject == gameObject);

        StartCoroutine(Timeout());
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(timeout);
        //ballManager.ChangeBall(thisBall, BallType.Normal);
        BallManager.Instance.ChangeBall(thisBall, BallType.Normal);
    }
}
