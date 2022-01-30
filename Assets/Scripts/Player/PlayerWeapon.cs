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

        audioSource = GetComponent<AudioSource>();
    }
    #endregion

    private Inventory inventory;
    private AudioSource audioSource;

    public AudioClip equipAudio;
    public Weapon[] currentWeapons;
    public Weapon defaultMelee;
    public Weapon defaultGun;

    public delegate void OnWeaponChanged(Weapon newItem, Weapon oldItem);
    public OnWeaponChanged onWeaponChanged;

    // Start is called before the first frame update
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(WeaponSlot)).Length;
        currentWeapons = new Weapon[numSlots];
        inventory = Inventory.instance;

        // Equip defaults
        currentWeapons[0] = defaultMelee;
        currentWeapons[1] = defaultGun;
        onWeaponChanged?.Invoke(null, null);
    }

    public void Equip(Weapon newItem)
    {
        audioSource.PlayOneShot(equipAudio, 10f);
        int slotIndex = (int)newItem.weaponType;

        Weapon oldItem = null;
        if (currentWeapons[slotIndex] != null)
        {
            oldItem = currentWeapons[slotIndex];
            inventory.Add(oldItem);
        }
        
        currentWeapons[slotIndex] = newItem;

        // Update UI
        onWeaponChanged?.Invoke(newItem, oldItem);
    }

    // Remove an item from player's weapons and add to inventory 
    public void Unequip(int slotIndex)
    {
        if (currentWeapons[slotIndex] != null)
        {
            Weapon oldItem = currentWeapons[slotIndex];
            inventory.Add(oldItem);
            
            if (slotIndex == 0)
            {
                currentWeapons[slotIndex] = defaultMelee;
            }
            else if (slotIndex == 1)
            {
                currentWeapons[slotIndex] = defaultGun;
            }
            

            // Update UI
            onWeaponChanged?.Invoke(null, oldItem);
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
