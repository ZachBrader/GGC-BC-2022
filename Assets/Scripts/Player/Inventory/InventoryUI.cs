using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent; // Searches children for inventorySlot scripts
    public GameObject inventoryUI; // Uses this to toggle inventory on and off

    InventoryBehavior inventory;
    InventorySlot[] inventorySlots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryBehavior.instance;
        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void OnEnable()
    {
        InventoryBehavior.onItemChangedCallback += UpdateUI;
    }

    private void OnDisable()
    {
        InventoryBehavior.onItemChangedCallback -= UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.Items.Count)
            {
                inventorySlots[i].AddItem(inventory.Items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
