using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 9;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void TakeDamage(int damage)
    {
        // Enemy looses health
        health -= damage;
        Debug.Log(health);

        // Check if enemy has died
        if (health <= 0)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        Debug.Log("Player has died");
    }
}
