using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Inventory inventory;
    Weapon[] currentWeapons;

    public Weapon melee;
    public Weapon ranged;

    public delegate void OnWeaponChanged(Weapon newItem, Weapon oldItem);
    public OnWeaponChanged onWeaponChanged;

    // Start is called before the first frame update
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(WeaponSlot)).Length;
        currentWeapons = new Weapon[numSlots];
        inventory = Inventory.instance;
    }

    public void Equip(Weapon newItem)
    {
        int slotIndex = (int)newItem.weaponType;

        Weapon oldItem = null;
        if (currentWeapons[slotIndex] != null)
        {
            oldItem = currentWeapons[slotIndex];
            inventory.Add(oldItem);
        }
        if (onWeaponChanged != null)
        {
            onWeaponChanged.Invoke(newItem, oldItem);
        }

        currentWeapons[slotIndex] = newItem;

    }

    public void Unequip(int slotIndex)
    {
        if (currentWeapons[slotIndex] != null)
        {
            Weapon oldItem = currentWeapons[slotIndex];
            inventory.Add(oldItem);

            if (onWeaponChanged != null)
            {
                onWeaponChanged.Invoke(null, oldItem);
            }

            currentWeapons[slotIndex] = null;
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentWeapons.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
