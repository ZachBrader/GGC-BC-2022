using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : Interactable
{
    // Private
    AudioSource audioSource;
    Animator animator;
    
    // Public
    public Item item;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();

        audioSource.Play();

        Pickup();
    }

    void Pickup()
    {
        // Play Animation
        animator.Play("Chest Open");

        // Add Item to inventory
        bool wasPickedUp = Inventory.instance.Add(item);

        // Destroy if item was picked up
        if (wasPickedUp)
        {
            this.enabled = false;
            promptManager?.ClosePrompt();
            playerController.RemoveFocus();
            Destroy(this.gameObject, 1f);
        }

    }
}
