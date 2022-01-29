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
        // Enemy looses health
        health -= damage;

        // Update Health Bar
        onHealthChanged?.Invoke();
        

        // Check if enemy has died
        /*if (health <= 0)
        {
            PlayerDeath();
        }*/
    }
    
    void PlayerDeath()
    {
        //Debug.Log("Player has died");

        // Play Player Death Sound
        audioSource.Play();

        // End Game
        gameManager.EndGame();
    }
}
