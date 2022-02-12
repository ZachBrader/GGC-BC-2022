using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCage : Interactable
{
    // Private
    AudioSource audioSource;
    GoalManager goalManager;
    Inventory inventory;

    // Public
    public GameObject puppyPrefab;

    private void Awake()
    {
        goalManager = GoalManager.instance;
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();
        inventory = Inventory.instance;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();

        Unlock();
    }

    void Unlock()
    {
        Debug.Log("Unlock");
        Debug.Log(inventory);
        if (inventory.RemoveKey())
        {
            Debug.Log("Puppy is free");
            Instantiate(puppyPrefab, transform.position, transform.rotation);

            goalManager?.dogCages.Remove(this);
            hasInteracted = true;
            promptManager?.ClosePrompt();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Cannot remove key");
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
