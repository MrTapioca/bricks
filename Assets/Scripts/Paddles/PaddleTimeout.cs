using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleTimeout : MonoBehaviour
{
    public float timeout = 5;

    //private PaddleManager paddleManager;

    void Start()
    {
        //paddleManager = GameObject.Find("Level Manager").GetComponent<PaddleManager>();

        StartCoroutine(Timeout());
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(timeout);
        
        // Release balls if paddle was magnet and balls were magnetically attached
        foreach (Ball ball in BallManager.Instance.Balls)
        {
            if (ball.BallMovement.attachedToPaddle && ball.BallMovement.AttachedByMagnet)
                ball.BallMovement.DetachFromPaddle();
        }

        PaddleManager.Instance.ChangePaddle(PaddleType.Normal);
    }
}