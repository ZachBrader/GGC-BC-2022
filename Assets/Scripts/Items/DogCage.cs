using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCage : MonoBehaviour
{
    // Private
    AudioSource audioSource;

    Inventory inventory;
    bool hasInteracted = false;

    PlayerController playerController;
    Transform playerPosition;

    // Public
    public GameObject puppyPrefab;
    public Item key;
    public float radius = 3f; // How close player needs to be to object
    public Transform interactionTransform;

    public bool canInteract = false;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
        playerPosition = player.transform;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inventory = Inventory.instance;
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerPosition.position, interactionTransform.position);
        if (distance <= radius)
        {
            Unlock();
        }

    }

    void Unlock()
    {
        if (!hasInteracted)
        {
            if (inventory.Remove(key))
            {
                GameObject puppy = Instantiate(puppyPrefab, transform.position, transform.rotation) as GameObject;
                Destroy(this.gameObject);
                hasInteracted = true;
            }
        }
        
    }
}
