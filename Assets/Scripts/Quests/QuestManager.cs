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
    }
    #endregion

    public int MAX_QUESTS = 5;
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    public static event Action onQuestsChanged;

    private void Start()
    {
        // Add Quests under the quest manager object
        Quest[] foundQuests = this.transform.GetComponentsInChildren<Quest>();

        // Add them accordingly
        foreach (Quest quest in foundQuests)
        {
            AddActiveQuest(quest);
        }
    }

    public bool AddActiveQuest(Quest quest)
    {
        // Don't add if we are at max
        if (activeQuests.Count >= MAX_QUESTS)
        {
            return false;
        }

        // Add to active quests list
        activeQuests.Add(quest);

        // Update subscribers
        onQuestsChanged?.Invoke();

        return true;
    }

    public bool RemoveActiveQuest(Quest quest)
    {
        // Attempt to remove
        bool removeQuest = activeQuests.Remove(quest);

        // Send subscribers update
        onQuestsChanged?.Invoke();

        // Return result of remove
        return removeQuest;
    }

    public bool AddCompletedQuest(Quest quest)
    {
        // Add to completed quests list
        completedQuests.Add(quest);
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
