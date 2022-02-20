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

    private Queue<string> charactersSpeaking;
    private Queue<string> sentences;

    public TMP_Text charNameText;
    public TMP_Text dialogueText;

    public Animator anim;

    public GameObject dialogueParent;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        charactersSpeaking = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        anim?.SetBool("isOpen", true);

        // dialogueParent.SetActive(true); // Turn on the dialogue panel
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string character in dialogue.characters)
        {
            charactersSpeaking.Enqueue(character);
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
        string charName = charactersSpeaking.Dequeue();
        charNameText.text = charName;
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        charNameText.text = "";
        dialogueText.text = "";
        anim.SetBool("isOpen", false);
        // dialogueParent.SetActive(false); // Turn off the dialogue
    }

    public bool getIsOpen()
    {
        return anim.GetBool("isOpen");
    }
}
