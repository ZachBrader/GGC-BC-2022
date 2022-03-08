using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    // Private
    AudioSource audioSource;
    
    // Public
    public Item item;

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
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
        // Add Item to inventory
        bool wasPickedUp = InventoryBehavior.instance.Add(item);

        // Destroy if item was picked up
        if (wasPickedUp)
        {
            promptManager?.ClosePrompt();
            playerController.RemoveFocus();
            Destroy(this.gameObject);
        }

    }
}
