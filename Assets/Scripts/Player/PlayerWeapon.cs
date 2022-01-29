using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region Singleton
    public static PlayerWeapon instance;

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

    private Inventory inventory;
    public Weapon[] currentWeapons;

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
        
        currentWeapons[slotIndex] = newItem;

        // Update UI
        if (onWeaponChanged != null)
        {
            onWeaponChanged.Invoke(newItem, oldItem);
        }
    }

    // Remove an item from player's weapons and add to inventory 
    public void Unequip(int slotIndex)
    {
        if (currentWeapons[slotIndex] != null)
        {
            Weapon oldItem = currentWeapons[slotIndex];
            inventory.Add(oldItem);
            
            currentWeapons[slotIndex] = null;

            // Update UI
            if (onWeaponChanged != null)
            {
                Debug.Log("Unequip Change");
                onWeaponChanged.Invoke(null, oldItem);
            }
        }
    }

    // Remove all weapons
    public void UnequipAll()
    {
        for (int i = 0; i < currentWeapons.Length; i++)
        {
            Unequip(i);
        }
    }

    // Test function to remove weapons
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
