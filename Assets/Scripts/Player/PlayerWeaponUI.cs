using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponUI : MonoBehaviour
{
    public Transform itemsParent; // Searches children for inventorySlot scripts
    public GameObject inventoryUI; // Uses this to toggle inventory on and off

    PlayerWeapon playerWeapon;
    InventorySlot[] weaponSlots;

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = PlayerWeapon.instance;
        playerWeapon.onWeaponChanged += UpdateUI;

        weaponSlots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Updates UI so player can see what items they have
    void UpdateUI(Weapon newItem, Weapon oldItem)
    {
        // Handle melee weapons
        if (playerWeapon.currentWeapons[0] != null)
        {
            weaponSlots[0].AddItem(playerWeapon.currentWeapons[0]);
        }
        else
        {
            weaponSlots[0].ClearSlot();
        }

        // Handle ranged weapons
        if (playerWeapon.currentWeapons[1] != null)
        {
            weaponSlots[1].AddItem(playerWeapon.currentWeapons[1]);
        }
        else
        {
            weaponSlots[1].ClearSlot();
        }
    }
}