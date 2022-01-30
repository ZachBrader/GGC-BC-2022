using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Weapon", fileName = "Weapon")]
public class Weapon : Item
{
    public int DamageModifier = 0; // Damage of weapon
    public float range = 15f; // Distance of weapon
    public float fireRate = 1f; // Time between swings
    public WeaponSlot weaponType; // Weapon Type
    public bool isNotEquipped = true;

    // When you click on the inventory slot
    public override void Use()
    {
        if (isNotEquipped)
        {
            isNotEquipped = false;
            base.Use();

            PlayerWeapon.instance.Equip(this);

            RemoveFromInventory();
        }
    }
}

public enum WeaponSlot { Melee, Ranged }
