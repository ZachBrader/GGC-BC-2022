using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    // Chest dropped to pick up item
    public GameObject chestPrefab;

    // Dropped Item
    public List<Item> itemsToDrop;

    private bool hasDroppedItem = false;

    public void DropItem()
    {
        // If no item has been dropped before
        if (!hasDroppedItem)
        {
            int randItem = Random.Range(0, itemsToDrop.Count);

            ChestInteract chest = Instantiate(chestPrefab, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation).GetComponent<ChestInteract>();
            chest.item = itemsToDrop[randItem];
        }
    }

}
