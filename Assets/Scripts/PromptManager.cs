using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptManager : MonoBehaviour
{
    #region Singleton
    public static PromptManager instance;

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

    public TMP_Text promptText;
    public GameObject promptParent;

    public void ShowPrompt(string nPrompt)
    {
        promptParent.SetActive(true);
        promptText.text = nPrompt;
    }

    public void ClosePrompt()
    {
        promptParent.SetActive(false);
    }


    public string GetCurrentPrompt()
    {
        return promptText.text;
    }
}
