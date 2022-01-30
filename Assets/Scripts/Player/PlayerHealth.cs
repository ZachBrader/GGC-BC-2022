using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (PlayerController))]
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

    [Tooltip("How long should the player be invincible after taking damage?")]
    public float invincibilityTime;
    [Tooltip("How quickly should the player blink after taking damage.")]
    public float blinkFrequency;
    private float timeSinceDamaged; //time since the player was last damaged

    private bool isDead = false;

    private AudioSource audioSource;
    private GameManager gameManager;
    private PlayerController playerController;

    public delegate void OnHealthChanged();
    public OnHealthChanged onHealthChanged;


    void Start()
    {
        gameManager = GameManager.instance;
        audioSource = GetComponent<AudioSource>(); // Set audio component
        playerController = GetComponent<PlayerController>();
        //offset the initial value of timeSinceDamaged so that the player is
        //immediately vulnerable
        timeSinceDamaged = invincibilityTime; 
    }

    private void Update()
    {
        //update timer variable
        timeSinceDamaged += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        //check if player is within invincibility window
        //or if the player is dead
        if(invincibilityTime >= timeSinceDamaged || isDead)
        {
            //do nothing if the player should be invincible / dead
            Debug.Log("Post-Damage Player Immunity");
            return;
        }



        if (!isDead)
        {
            // Player looses health
            health -= damage;

            // Update Health Bar
            onHealthChanged?.Invoke();


            // Check if player has died
            if (health <= 0)
            {
                PlayerDeath();
            }
            else
            {
                //if player not dead
                StartCoroutine(BlinkRoutine());
                timeSinceDamaged = 0;

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

    IEnumerator BlinkRoutine()
    {
        //the time the blinking should stop
        float endTime = Time.time + invincibilityTime;
        float lastBlinkTime = 0;
        Renderer[] renderers = GameObject.Find("Player/Graphics").GetComponentsInChildren<Renderer>();

        while (Time.time < endTime)
        {
            // ---1/(2*blinkFrequency) notes---
            //Blink frequency should be number of times the player blinks on and off in a second,
            //hence 1/frequency. However, each time the if statement is true, the player alternates
            //their visibilty -- that means that the if statement needs to fire twice in order
            //to complete 1 blink, hence 1/(2frequency).

            if (Time.time - lastBlinkTime >= 1/ (2*blinkFrequency))
            {
                //flip visibility
                foreach (Renderer renderer in renderers)
                    renderer.enabled = !renderer.enabled;
                lastBlinkTime = Time.time; // update last blink time
            }

            yield return new WaitForEndOfFrame();

        }

        //blinking is done
        foreach (Renderer renderer in renderers)
            renderer.enabled = true; //turn on renderer
    }
}
