using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // Icon describing item
    public Button removeButton; 

    Item item; // Item in slot

    // Add Item to slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        if (item.icon != null)
        {
            icon.sprite = item.icon;
        }
        icon.enabled = true;

        removeButton.interactable = true;
    }

    // Remove items from slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    // Use item
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
