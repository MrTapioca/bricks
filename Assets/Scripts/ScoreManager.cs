using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    public Text scoreText;

    public int Score { get; private set; }

    //private int brickHitMultiplier = 1;

    public void AddPoints(ScoreAction action, int multiplier = 1)
    {
        int points = 0;
        switch (action)
        {
            case ScoreAction.BrickHit:
                points = 10;
                break;
            case ScoreAction.PowerupGrab:
                points = 5;
                break;
        }
        
        Score += points * multiplier;
        RefreshScoreDisplay();
    }

    //public void IncreaseMultiplier(ScoreAction action)
    //{
    //    switch (action)
    //    {
    //        case ScoreAction.BrickHit:
    //            brickHitMultiplier++;
    //            break;
    //    }
    //}

    //public void ResetMultiplier(ScoreAction action)
    //{
    //    switch (action)
    //    {
    //        case ScoreAction.BrickHit:
    //            brickHitMultiplier = 1;
    //            break;
    //    }
    //}

    private void RefreshScoreDisplay()
    {
        scoreText.text = Score.ToString();
    }
}

public enum ScoreAction
{
    BrickHit,
    PowerupGrab
}