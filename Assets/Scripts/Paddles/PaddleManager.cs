using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PaddleType
{
    Normal,
    Small,
    Extended,
    Laser,
    Magnet
}

public class PaddleManager : Singleton<PaddleManager>
{
    public GameObject normal;
    public GameObject small;
    public GameObject extended;
    public GameObject laser;
    public GameObject magnet;

    public PaddleType currentType;
    public GameObject currentPaddle;
    public Rigidbody2D currentPaddleBody;
    public BoxCollider2D currentPaddleCollider;

    public int extraLives = 2;

    public delegate void PaddleChangingAction(PaddleType newType, GameObject newPaddle, Rigidbody2D newPaddleBody);
    public static event PaddleChangingAction OnPaddleChanging;

    public delegate void PaddleLostAction();
    public static event PaddleLostAction OnPaddleLost;

    public void LosePaddle()
    {
        extraLives--;
        OnPaddleLost?.Invoke();
    }

    public void GainPaddle(int amount = 1)
    {
        extraLives = amount;
        OnPaddleLost?.Invoke();
    }

    public void ChangePaddle(PaddleType newType)
    {
        // Create new paddle
        GameObject newPaddle = Instantiate(GetPrefab(newType), currentPaddle.transform.position, currentPaddle.transform.rotation);
        newPaddle.name = "Paddle";
        Rigidbody2D newPaddleBody = newPaddle.GetComponent<Rigidbody2D>();
        BoxCollider2D newPaddleCollider = newPaddle.GetComponent<BoxCollider2D>();

        // Alert subscribed scripts about new paddle
        OnPaddleChanging?.Invoke(newType, newPaddle, newPaddleBody);

        // Destroy and replace current paddle
        Destroy(currentPaddle);

        currentType = newType;
        currentPaddle = newPaddle;
        currentPaddleBody = newPaddleBody;
        currentPaddleCollider = newPaddleCollider;
    }
    
    private GameObject GetPrefab(PaddleType type)
    {
        switch (type)
        {
            case PaddleType.Normal: return normal;
            case PaddleType.Small: return small;
            case PaddleType.Extended: return extended;
            case PaddleType.Laser: return laser;
            case PaddleType.Magnet: return magnet;
            default: return null;
        }
    }
}
