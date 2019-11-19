using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPaddle : MonoBehaviour
{
    public GameObject laser;
    public float rate = 1;

    private Transform leftEmitter;
    private Transform rightEmitter;

    void Start()
    {
        leftEmitter = transform.Find("Left Emitter");
        rightEmitter = transform.Find("Right Emitter");


        StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            Instantiate(laser, leftEmitter.position, Quaternion.identity);
            Instantiate(laser, rightEmitter.position, Quaternion.identity);

            yield return new WaitForSeconds(rate);
        }
    }
}