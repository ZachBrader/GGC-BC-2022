using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Item
{
    public int DamageModifier = 0;
    public float Range = 2f;

    public override void Use()
    {
        base.Use();

        // EquipmentManager.instance.Equip(this);

        RemoveFromInventory();
    }
}
