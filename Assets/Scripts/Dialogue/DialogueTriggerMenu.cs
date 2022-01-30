using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMenu : MonoBehaviour
{
    public Dialogue dialogue;

    private DialogueManager dialogueManager;
    void Start()
    {
        dialogueManager = DialogueManager.instance;
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
