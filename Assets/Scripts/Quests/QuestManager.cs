using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton
    public static QuestManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Instances Detected");
            return;
        }
        instance = this;
        Debug.Log("Assigned Goal Manager to instance");
    }
    #endregion

    public int MAX_QUESTS = 5;
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    public static event Action onQuestsChanged;

    private void Start()
    {
        Quest[] foundQuests = this.transform.GetComponentsInChildren<Quest>();

        foreach (Quest quest in foundQuests)
        {
            Debug.Log(quest.information.name);
            Debug.Log(AddActiveQuest(quest));

        }
    }

    public bool AddActiveQuest(Quest quest)
    {
        if (activeQuests.Count >= MAX_QUESTS)
        {
            return false;
        }

        activeQuests.Add(quest);

        onQuestsChanged?.Invoke();

        return true;
    }

    public bool RemoveActiveQuest(Quest quest)
    {
        bool removeQuest = activeQuests.Remove(quest);

        onQuestsChanged?.Invoke();

        return removeQuest;
    }

    public bool AddCompletedQuest(Quest quest)
    {
        activeQuests.Add(quest);
        return true;
    }

    public bool FinishQuest(Quest quest)
    {
        // Ensure Quest exists in active list
        bool isActiveQuest = activeQuests.Contains(quest);
        if (!isActiveQuest)
        {
            return false;
        }

        // Remove from active list and add to completed list
        bool success = RemoveActiveQuest(quest) && AddCompletedQuest(quest);

        return success;
    }
}
