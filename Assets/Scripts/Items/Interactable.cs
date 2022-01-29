using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f; // How close player needs to be to object
    public Transform interactionTransform;

    public bool canInteract = false;
    protected bool hasInteracted = false;

    protected PlayerController playerController;
    protected Transform playerPosition;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
        playerPosition = player.transform;
    }


    private void Update()
    {
        float distance = Vector3.Distance(playerPosition.position, interactionTransform.position);
        if (distance <= radius)
        {
            playerController.SetFocus(this);
        }

        if (playerController.focus == this)
        {
            if (distance > radius)
            {
                playerController.RemoveFocus();
            }
        }
        
    }

    public virtual bool OnInteract()
    {
        if (canInteract && !hasInteracted)
        {
            float distance = Vector3.Distance(playerPosition.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

}
