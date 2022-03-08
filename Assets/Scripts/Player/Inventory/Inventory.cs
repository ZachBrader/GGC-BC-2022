using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int capacity; // Max size of inventory
    public List<Item> items { get; private set; } // Holds inventory items

    public Inventory(int totalCapacity)
    {
        items = new List<Item>();

        capacity = totalCapacity;
    }

    public bool CanAddItem()
    {
        return items.Count + 1 < capacity;
    }

    public bool AddItem(Item item)
    {
        if (!CanAddItem()) return false;
        items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item) => items.Remove(item);
}
