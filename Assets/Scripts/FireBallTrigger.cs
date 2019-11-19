using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrigger : MonoBehaviour
{
    //private BallManager ballManager;

    private void Awake()
    {
        //ballManager = GameObject.Find("Level Manager").GetComponent<BallManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BallManager.Instance.Balls.Exists(ball => ball.GameObject == collision.gameObject && ball.Type == BallType.Fire))
        {
            var blockScript = transform.parent.gameObject.GetComponent<BreakableBlock>();
            blockScript.DestroyBlock();
        }
    }
}