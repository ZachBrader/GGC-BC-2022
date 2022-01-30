using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Health", fileName = "Health")]
public class HealthPack : Item
{
    public int Heal = 1;

    // When you click on the inventory slot
    public override void Use()
    {
        base.Use();

        PlayerHealth.instance.GainHealth(Heal);

        RemoveFromInventory();
    }
}
