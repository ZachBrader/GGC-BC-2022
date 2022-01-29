using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Item
{
    public int DamageModifier = 0;
    public float Range = 15f; // Distance of bullets
    public float fireRate = 1f; // Time between shots


    public override void Use()
    {
        base.Use();

        // EquipmentManager.instance.Equip(this);

        RemoveFromInventory();
    }
}
