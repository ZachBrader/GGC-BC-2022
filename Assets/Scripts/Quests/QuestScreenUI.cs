using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScreenUI : MonoBehaviour
{
    #region Singleton
    public static QuestScreenUI instance;

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

    QuestManager questManager;
    Quest selectedQuest;

    public GameObject questUIParent;
    bool isOpen;

    public TMP_Text questName;
    public TMP_Text questDescription;
    public TMP_Text goalText;

    public GameObject questList;
    QuestSelector[] questSelectors;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to quests event
        QuestManager.onQuestsChanged += UpdateQuestMenu;

        // Keep track of quest manager's singleton
        questManager = QuestManager.instance;
        if (questManager.activeQuests.Count > 0)
        {
            SelectNewQuest(questManager.activeQuests[0]);
            UpdateSelectedQuestView();
        }

        questSelectors = questList.GetComponentsInChildren<QuestSelector>();

        isOpen = questUIParent.activeSelf;

        UpdateQuestSelectors();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!isOpen) OpenQuestMenu();
            else CloseQuestMenu();
        }
    }

    public void SelectNewQuest(Quest newQuest)
    {
        selectedQuest = newQuest;
        UpdateQuestMenu();
    }

    public void OpenQuestMenu()
    { 
        isOpen = true;
        questUIParent.SetActive(true);
        SelectNewQuest(questManager.activeQuests[0]);
    }

    public void CloseQuestMenu()
    {
        isOpen = false;
        questUIParent.SetActive(false);
    }

    public void UpdateQuestMenu()
    {
        UpdateSelectedQuestView();
        UpdateQuestSelectors();
    }    

    public void UpdateSelectedQuestView()
    {
        if (selectedQuest != null)
        {
            questName.text = selectedQuest.information.name;
            questDescription.text = selectedQuest.information.description;

            string questGoalText = "";
            foreach (Quest.Goal goal in selectedQuest.goals)
            {
                questGoalText += goal.GetDetails() + "\n";
            }
            goalText.text = questGoalText;
        }
    }

    public void UpdateQuestSelectors()
    {
        int count = 0;

        foreach (Quest quest in questManager.activeQuests)
        {
            questSelectors?[count].ChangeAssignedQuest(quest);
            count++;
        }
    }
}
