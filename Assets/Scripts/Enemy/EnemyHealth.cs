using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    ItemDrop itemDrop;

    private void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
    }

    public void TakeDamage(int damage)
    {
        // Enemy looses health
        health -= damage;

        // Check if enemy has died
        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        itemDrop.DropItem();
        Destroy(gameObject);
    }
}
