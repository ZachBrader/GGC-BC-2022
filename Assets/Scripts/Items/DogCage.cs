using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCage : MonoBehaviour
{
    // Private
    AudioSource audioSource;
    GoalManager goalManager;
    Inventory inventory;
    bool hasInteracted = false;

    PlayerController playerController;
    Transform playerPosition;
    PromptManager promptManager;

    // Public
    public string promptMessage = "Key Required";
    public GameObject puppyPrefab;
    public float radius = 30f; // How close player needs to be to object
    public Transform interactionTransform;

    public bool canInteract = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
        playerPosition = player.transform;

        promptManager = PromptManager.instance;
        goalManager = GoalManager.instance;
        audioSource = GetComponent<AudioSource>();
        inventory = Inventory.instance;
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerPosition.position, interactionTransform.position);
        if (distance <= radius)
        {
            Debug.Log("Use a key");
            promptManager?.ShowPrompt(promptMessage);
            Unlock();
        }
        else
        {
            promptManager?.ClosePrompt();
        }

    }

    void Unlock()
    {
        if (!hasInteracted)
        {
            if (inventory.RemoveKey())
            {
                Instantiate(puppyPrefab, transform.position, transform.rotation);

                goalManager.dogCages.Remove(this);
                hasInteracted = true;
                promptManager?.ClosePrompt();
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
