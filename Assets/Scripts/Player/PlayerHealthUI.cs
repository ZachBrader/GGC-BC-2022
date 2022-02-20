using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public Transform itemsParent; // Searches children for inventorySlot scripts
    public GameObject inventoryUI; // Uses this to toggle inventory on and off

    PlayerHealth playerHealth;
    Image[] healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = PlayerHealth.instance;
        playerHealth.onHealthChanged += UpdateUI;
        healthPoints = itemsParent.GetComponentsInChildren<Image>();
    }

    // Updates UI so player can see what items they have
    void UpdateUI()
    {
        for (int i = 0; i < PlayerHealth.MAX_HEALTH; i++)
        {
            if (i < playerHealth.health)
            {
                healthPoints[i].enabled = true;
            }
            else
            {
                healthPoints[i].enabled = false;
            }
            
        }
    }
}
