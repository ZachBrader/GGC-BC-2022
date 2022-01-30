using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Singleton
    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Instances Detected");
            return;
        }
        instance = this;
    }
    #endregion

    public int health = 9;
    public int MAX_HEALTH = 9;

    private bool isDead = false;

    private AudioSource audioSource;
    private GameManager gameManager;

    public delegate void OnHealthChanged();
    public OnHealthChanged onHealthChanged;

    void Start()
    {
        gameManager = GameManager.instance;
        audioSource = GetComponent<AudioSource>(); // Set audio component
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            // Player looses health
            health -= damage;

            // Update Health Bar
            onHealthChanged?.Invoke();
        

            // Check if enemy has died
            if (health <= 0)
            {
                PlayerDeath();
            }
        }
    }

    public void GainHealth(int heal)
    {
        if (!isDead)
        {
            // Lock health based on MAX_HEALTH
            if (health + heal <= MAX_HEALTH)
            {
                // Player gains health
                health += heal;

                // Update Health Bar
                onHealthChanged?.Invoke();
            }
            else
            {
                health = MAX_HEALTH;
            }
            
        }
    }
    
    void PlayerDeath()
    {
        isDead = true;
        //Debug.Log("Player has died");

        // Play Player Death Sound
        audioSource.Play();

        // End Game
        gameManager.EndGame();
    }
}
