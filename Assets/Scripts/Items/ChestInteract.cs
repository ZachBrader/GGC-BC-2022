using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up " + item.name);

        // Add Item to inventory
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            Destroy(this.gameObject);
        }

    }
}