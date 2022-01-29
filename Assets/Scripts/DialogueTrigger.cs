using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue; // Text to print out
    private DialogueManager dialogueManager; // Reference to dialogue manager

    void Start()
    {
        dialogueManager = DialogueManager.instance;
    }

    public override bool OnInteract()
    {
        if (canInteract)
        {
            float distance = Vector3.Distance(playerPosition.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                return true;
            }
        }
        return false;
    }

    public override void Interact()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
