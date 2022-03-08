using UnityEngine;
using System;
using System.Collections.Generic;

public class InventoryBehavior : MonoBehaviour
{
    #region Singleton
    public static InventoryBehavior instance;

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

    #region Editor Variables

    public int space = 5; // Max size of inventory
    private Inventory inventory;

    #endregion

    public List<Item> Items => inventory.items; // Reference to inventory list
    public static event Action onItemChangedCallback; // Hook for when inventory changes


    private void Start()
    {
        inventory = new Inventory(space);
    }

    #region Inventory Functions

    // Add an item to inventory
    public bool Add(Item item)
    {
        bool success = inventory.AddItem(item);
        onItemChangedCallback?.Invoke();
        return success;
    }

    // Remove an item from the inventory
    public bool Remove(Item item)
    {
        bool success = inventory.RemoveItem(item);
        onItemChangedCallback?.Invoke();
        return success;
    }

    public bool RemoveKey()
    {
        foreach (Item item in inventory.items)
        {
            if (item.name == "Key")
            {
                return Remove(item);
            }
        }

        return false;
    }
    #endregion
}
