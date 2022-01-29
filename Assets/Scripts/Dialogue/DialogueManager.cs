using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance;

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

    private Queue<string> sentences;

    public TMP_Text charNameText;
    public TMP_Text dialogueText;

    public GameObject dialogueParent;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueParent.SetActive(true); // Turn on the dialogue panel
        charNameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        charNameText.text = "";
        dialogueText.text = "";
        dialogueParent.SetActive(false); // Turn off the dialogue
    }
}
