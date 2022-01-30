using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    // Private
    AudioSource audioSource;
    
    // Public
    public Item item;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        bool wasPickedUp = Inventory.instance.Add(item);

        // Destroy if item was picked up
        if (wasPickedUp)
        {
            Destroy(this.gameObject);
        }

    }
}
