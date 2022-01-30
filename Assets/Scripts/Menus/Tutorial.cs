using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public Transform startTutorialButton;
    public Transform tutorialParent; // Controls Tutorial
    public Transform tutorialUIParent; // Controls Showing Player UI
    public Transform optionParent;
    public Transform dialogueParent;
    public Transform dialogueTriggerParent;
    public Transform dialogueContinueButton;


    private bool lockDialogue = false;
    private int curDialogue = 0;
    private DialogueTriggerMenu[] dialogueTriggers;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTriggers = dialogueTriggerParent.GetComponentsInChildren<DialogueTriggerMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (curDialogue == 0)
        {
            lockDialogue = true;
        }
        else
        {
            lockDialogue = false;
        }

        if (lockDialogue)
        {
            dialogueContinueButton.gameObject.SetActive(false);
        }
        else
        {
            dialogueContinueButton.gameObject.SetActive(true);
        }

        if (!optionParent.gameObject.activeSelf && curDialogue == 1)
        {
            CloseStartTutorialButton();
            OpenPlayerOption();
        }

        if (tutorialParent.gameObject.activeSelf && curDialogue == 3)
        {
            tutorialParent.gameObject.SetActive(false);
            tutorialUIParent.gameObject.SetActive(true);
        }

        if (dialogueParent.gameObject.activeSelf == false)
        {
            TriggerDialogue();
            OpenDialogue();
        }
    }


    public void TriggerDialogue()
    {
        if (curDialogue == 9)
        {

            PlayerEndTutorial();
        }
        else
        {
            dialogueTriggers[curDialogue].TriggerDialogue();
            curDialogue += 1;

            Debug.Log("Next Scene: " + curDialogue.ToString());
        }
        
    }

    public void CloseStartTutorialButton()
    {
        startTutorialButton.gameObject.SetActive(false);
    }

    public void OpenDialogue()
    {
        dialogueParent.gameObject.SetActive(true);
    }

    public void CloseDialogue()
    {
        dialogueParent.gameObject.SetActive(false);
    }

    public void OpenPlayerOption()
    {
        optionParent.gameObject.SetActive(true);
        lockDialogue = true;
    }

    public void ClosePlayerOption()
    {
        optionParent.gameObject.SetActive(false);
        lockDialogue = false;
    }

    public void PlayerEndTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowPlayer()
    {
        tutorialParent.gameObject.SetActive(false);
        tutorialUIParent.gameObject.SetActive(false);
    }
}
