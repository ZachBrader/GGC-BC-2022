using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent (typeof (PlayerController))]
public class PlayerHealth : Health
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

    private AudioSource audioSource;

    public event Action onHealthChanged;


    protected override void Start()
    {
        // Initial health start
        base.Start();

        // Set audio component
        audioSource = GetComponent<AudioSource>(); 
    }

    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        onHealthChanged?.Invoke();
    }

    public override void GainHealth(int heal)
    {
        base.GainHealth(heal);

        // Update Health Bar
        onHealthChanged?.Invoke();
    }
    
    protected override void OnDeath()
    {
        base.OnDeath();

        // Play Player Death Sound
        audioSource.Play();

        // End Game
        gameManager.EndGame();
    }

    protected override void OnDamage()
    {
        base.OnDamage();

    }
}
