using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleGrid : MonoBehaviour
{
    public GameObject paddleIcon;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePaddleIcons();
    }

    private void OnEnable()
    {
        PaddleManager.OnPaddleLost += PaddleManager_OnPaddleLost;
    }

    private void OnDisable()
    {
        PaddleManager.OnPaddleLost -= PaddleManager_OnPaddleLost;
    }

    private void PaddleManager_OnPaddleLost()
    {
        UpdatePaddleIcons();
    }

    private void UpdatePaddleIcons()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < PaddleManager.Instance.extraLives; i++)
        {
            Instantiate(paddleIcon, transform);
        }
    }
}
