using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;

    public void TakeDamage(int damage)
    {
        // Enemy looses health
        health -= damage;
        Debug.Log(health);

        // Check if enemy has died
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
