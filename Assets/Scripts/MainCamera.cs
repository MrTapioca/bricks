using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject topWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject bottomWall;

    public GameObject inputArea;

    private void Start()
    {
        Camera mainCamera = GetComponent<Camera>();
        if (mainCamera.orthographic)
        {
            float orthographicSizeWide = mainCamera.orthographicSize * mainCamera.aspect;

            // Fix wall positions based on screen width
            topWall.transform.localScale = new Vector3(orthographicSizeWide * 2, 1, 1);
            leftWall.transform.Translate(-orthographicSizeWide - leftWall.transform.localPosition.x, 0, 0);
            rightWall.transform.Translate(orthographicSizeWide - rightWall.transform.localPosition.x, 0, 0);
            bottomWall.transform.localScale = new Vector3(orthographicSizeWide * 2, 1, 1);

            // Fix input area size based on screen width
            inputArea.transform.localScale = new Vector3(orthographicSizeWide * 2, inputArea.transform.localScale.y, 1);
        }
    }
}