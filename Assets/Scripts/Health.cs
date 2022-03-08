using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Health : MonoBehaviour
{
    // Invincibility variables
    [Tooltip("How long should the player be invincible after taking damage?")]
    public float invincibilityTime;
    [Tooltip("How quickly should the player blink after taking damage.")]
    public float blinkFrequency;
    private float timeSinceDamaged; // Time since the player was last damaged

    // Health variables
    public const int MAX_HEALTH = 9;
    public int health;
    protected bool isDead = false; // Determines if character is dead

    protected GameManager gameManager; // Reference to gamemanager

    protected virtual void Start()
    {
        gameManager = GameManager.instance;

        // Offset the initial value of timeSinceDamaged so that the player is immediately vulnerable
        timeSinceDamaged = invincibilityTime;

        health = MAX_HEALTH;
    }

    protected virtual void Update()
    {
        timeSinceDamaged += Time.deltaTime; // Update timer variable
    }


    public virtual void TakeDamage(int damage)
    {
        Debug.Log("Took " + damage + " damage");

        // No action taken if character is dead or invincible
        if (isDead || invincibilityTime >= timeSinceDamaged)
        {
            Debug.Log("No damage dealt");
            return;
        }

        // Player looses health
        health -= damage;

        // Check if player has died
        if (health <= 0)
        {
            OnDeath();
        }
        else
        {
            OnDamage();
        }
    }

    public virtual void GainHealth(int heal)
    {
        // No action taken if character is dead
        if (isDead)
        {
            return;
        }

        // Lock health based on MAX_HEALTH
        if (health + heal <= MAX_HEALTH)
        {
            // Player gains health
            health += heal;
        }
        else
        {
            health = MAX_HEALTH;
        }

    }

    protected virtual void OnDeath()
    {
        isDead = true;
        Debug.Log("Character " + transform.name + " is dead");
    }

    protected virtual void OnDamage()
    {
        Debug.Log("Character " + transform.name + " took damage");

        // Initialize player blink routine
        StartCoroutine(BlinkRoutine());

        // Reset timer
        timeSinceDamaged = 0;
    }

    protected IEnumerator BlinkRoutine()
    {
        Debug.Log("Blink routine starting");

        //the time the blinking should stop
        float endTime = Time.time + invincibilityTime;
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
