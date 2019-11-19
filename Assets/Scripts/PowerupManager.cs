using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : Singleton<PowerupManager>
{
    public GameObject[] powerups;
    public int diceRollSides = 5;

    public void RollPowerupChance(Vector3 origin)
    {
        // Decide whether to create a random powerup
        if (Random.Range(0, diceRollSides) == 0)
        {
            // Instantiate random powerup
            var powerupIndex = Random.Range(0, powerups.Length);

            GameObject powerup = Instantiate(powerups[powerupIndex], origin, Quaternion.identity);

            // Set initial velocity
            //Rigidbody2D powerupBody = powerup.GetComponent<Rigidbody2D>();
            //powerupBody.velocity = collision.relativeVelocity.normalized;
        }
    }
}