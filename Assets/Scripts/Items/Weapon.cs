using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int DamageModifier = 0; // Damage of weapon
    public float range = 15f; // Distance of weapon
    public float fireRate = 1f; // Time between swings
    public WeaponSlot weaponType; // Weapon Type

    public override void Use()
    {
        base.Use();

        // EquipmentManager.instance.Equip(this);

        RemoveFromInventory();
    }
}

public enum WeaponSlot { Melee, Ranged }
