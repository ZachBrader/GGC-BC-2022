using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSelector : MonoBehaviour
{
    public Quest quest;
    public TMP_Text questName;

    QuestScreenUI questMenu;

    // Start is called before the first frame update
    void Start()
    {
        questMenu = QuestScreenUI.instance;
    }

    public void ChangeAssignedQuest(Quest newQuest)
    {
        quest = newQuest;
        UpdateSelector();
    }

    public void UpdateSelector()
    {
        if (quest != null)
        {
            questName.text = quest.information.name;
        }
    }

    public void SelectQuest()
    {
        if (quest != null)
        {
            questMenu.SelectNewQuest(quest);
        }
    }
}
