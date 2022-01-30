using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

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

    public List<Item> items = new List<Item>(); // Holds inventory items

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback; // Hook for when inventory changes

    public int space = 5; // Max size of inventory

    // Add an item to inventory
    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room in the inventory");
            return false;
        }
        items.Add(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }

    // Remove an item from the inventory
    public bool Remove(Item item)
    {
        bool removeSuccessful = items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return removeSuccessful;
    }
}
