using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int speed = 1;

    private Rigidbody2D laserBody;
    private BoxCollider2D laserCollider;
    private Camera cam;

    void Start()
    {
        laserBody = GetComponent<Rigidbody2D>();
        laserCollider = GetComponent<BoxCollider2D>();
        cam = Camera.main;

        laserBody.velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Top Wall" || collision.gameObject.CompareTag("Block"))
        {
            laserBody.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        // Destroy object if it ends up out of bounds
        if (laserBody.position.y > cam.orthographicSize + (laserCollider.size.y - laserCollider.offset.y))
            Destroy(gameObject);
    }
}