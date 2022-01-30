using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    [Tooltip("Time the enemy is immune after taking damage")]
    public float immunityTime;
    [Tooltip("How fast should the enemy blink after taking damage?")]
    public float blinkFrequency;
    private float timeLastDamaged;


    ItemDrop itemDrop;

    private void Start()
    {
        itemDrop = GetComponent<ItemDrop>();
        timeLastDamaged = Time.time - immunityTime - 1; //offset -- enemy immedeately immune
    }

    public void TakeDamage(int damage)
    {
        if (!IsDamageable())
        {
            Debug.Log("Post damage enemy immunity");
            return;
        }

        // Enemy looses health
        health -= damage;

        // Check if enemy has died
        if (health <= 0)
        {
            EnemyDeath();
        }
        else
        {
            //enemy has taken damage, but isn't dead
            timeLastDamaged = Time.time;
            StartCoroutine(BlinkRoutine());
        }
    }

    void EnemyDeath()
    {
        itemDrop.DropItem();
        Destroy(gameObject);
    }

    private bool IsDamageable()
    {
        return Time.time - timeLastDamaged > immunityTime;
    }

    IEnumerator BlinkRoutine()
    {
        //the time the blinking should stop
        float endTime = Time.time + immunityTime;
        float lastBlinkTime = 0;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        while (Time.time < endTime)
        {
            // ---1/(2*blinkFrequency) notes---
            //Blink frequency should be number of times the player blinks on and off in a second,
            //hence 1/frequency. However, each time the if statement is true, the player alternates
            //their visibilty -- that means that the if statement needs to fire twice in order
            //to complete 1 blink, hence 1/(2frequency).

            if (Time.time - lastBlinkTime >= 1 / (2 * blinkFrequency))
            {
                //flip visibility
                foreach (Renderer r in renderers)
                {
                    r.enabled = !r.enabled;
                }

                lastBlinkTime = Time.time; // update last blink time
            }

            yield return new WaitForEndOfFrame();

        }

        //blinking is done, turn on all renderers
        foreach (Renderer r in renderers)
        {
            r.enabled = true; 
        }
    }
}
