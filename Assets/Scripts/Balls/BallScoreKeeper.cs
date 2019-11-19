using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoreKeeper : MonoBehaviour
{
    private BallMovement ballMovement;

    public int ScoreMultiplier { get; private set; }

    private void Start()
    {
        ballMovement = GetComponent<BallMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ballMovement.attachedToPaddle)
        {
            
        }
    }
}
