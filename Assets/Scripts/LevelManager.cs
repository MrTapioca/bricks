using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //public PaddleManager paddleManager;
    //public BallManager ballManager;
    public PaddleControls paddleControls;

    private Vector3 paddleStartPosition;
    private Vector3 ballStartPosition;

    private void Start()
    {
        paddleStartPosition = PaddleManager.Instance.currentPaddle.transform.position;
        ballStartPosition = BallManager.Instance.startupBall.transform.position;
    }

    public void ResetPaddleAndBall()
    {
        // TODO: Reset paddle to normal if necessary
        // Reset ball to normal if necessary

        paddleControls.CancelDrag();
        BallManager.Instance.Balls[0].BallMovement.AttachToPaddle();

        PaddleManager.Instance.currentPaddle.transform.position = paddleStartPosition;
        BallManager.Instance.Balls[0].GameObject.transform.position = ballStartPosition;
    }

    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}