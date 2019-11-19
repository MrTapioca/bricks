using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject hitParticles;
    public GameObject destroyParticles;
    public Color destroyParticleColor;

    public GameObject[] powerups;
    public int powerupDiceRollSides = 5;

    private SpriteRenderer spriteRenderer;
    private int currentHits;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // React to ball hitting the block
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Projectile"))
        {
            currentHits++;

            // Compare the current hit count to the sprite count
            if (currentHits > sprites.GetUpperBound(0))
            {
                DestroyBlock();
            }
            else
            {
                // The block is still not destroyed; switch sprites
                spriteRenderer.sprite = sprites[currentHits];

                // Create particle hit effect
                if (hitParticles != null)
                {
                    ContactPoint2D contact = collision.GetContact(0);
                    Vector3 position = contact.point;
                    //Quaternion rotation = Quaternion.FromToRotation(Vector3.back, contact.normal);
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.down, contact.normal);
                    GameObject particles = Instantiate(hitParticles, position, rotation);
                    var mainSettings = particles.GetComponent<ParticleSystem>().main;
                    mainSettings.startColor = new ParticleSystem.MinMaxGradient(destroyParticleColor);

                }
            }
        }
    }

    public void DestroyBlock()
    {
        Destroy(gameObject);

        // Create particle explosion
        if (hitParticles != null)
        {
            GameObject particles = Instantiate(destroyParticles, transform.position, Quaternion.identity);
            var mainSettings = particles.GetComponent<ParticleSystem>().main;
            mainSettings.startColor = new ParticleSystem.MinMaxGradient(destroyParticleColor);
        }

        PowerupManager.Instance.RollPowerupChance(transform.position);
    }
}