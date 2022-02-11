using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 30f; // How close player needs to be to object
    public Transform interactionTransform;

    public bool canInteract = false;
    public string promptMessage = "Press 'P' to interact";
    protected bool hasInteracted = false;
    protected PromptManager promptManager;

    protected PlayerController playerController;
    protected Transform playerPosition;

    protected virtual void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
           
        playerController = player.GetComponent<PlayerController>();
        playerPosition = player.transform;

        Debug.Log(PromptManager.instance);
        Debug.Log("This is what we are assigning");
        promptManager = PromptManager.instance;

        Debug.Log(promptManager);

        Debug.Log("Awoken interactable object");
    }

    protected virtual void Update()
    {
        float distance = Vector3.Distance(playerPosition.position, interactionTransform.position);
        if (distance <= radius)
        {
            promptManager?.ShowPrompt(promptMessage);
            playerController.SetFocus(this);
        }

        if (playerController.focus == this)
        {
            if (distance > radius)
            {
                promptManager?.ClosePrompt();
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
