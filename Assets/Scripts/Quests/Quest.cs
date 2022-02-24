using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    // Stores information on the quest
    [System.Serializable]
    public struct Info
    {
        public string name;
        public string description;
    }

    public Info information = new Info { name = "Quest", description = "Do the thing" };
    public bool isComplete = false;
    public bool isPrimary = false;


    public List<Goal> goals; // List of goals to complete quests

    public delegate void OnQuestComplete();
    public OnQuestComplete onQuestComplete;

    // Initialize quest
    public void Start()
    {
        isComplete = false;

        foreach (Goal goal in goals)
        {
            goal.Initialize();
            goal.onGoalComplete += CheckGoals;
        }
    }

    // Evaluate if quest is finished
    public void CheckGoals()
    {
        // Check each goal to ensure all goals are marked complete
        bool goalCheck = true;
        foreach (Goal goal in goals)
        {
            // If any goals are not complete, set goal check to false
            if (!goal.isComplete)
            {
                goalCheck = false;
                break;
            }
        }

        if (goalCheck)
        {
            isComplete = true;

            onQuestComplete?.Invoke();

            // Remove listerners
        }
    }

    [System.Serializable]
    public abstract class Goal : MonoBehaviour 
    {
        protected string description { get; set; } = "Default";
        public int currentAmount { get; protected set; }
        public int requiredAmount = 1;

        public bool isComplete;

        public delegate void OnGoalComplete();
        public OnGoalComplete onGoalComplete;

        public virtual void Initialize()
        {
            currentAmount = 0;
            isComplete = false;
        }

        public virtual void Increment(int value)
        {
            currentAmount += value;
            Evaluate();
        }

        public virtual void Evaluate()
        {
            if (currentAmount >= requiredAmount)
            {
                Complete();
            }
        }

        public virtual void Complete()
        {
            isComplete = true;
            onGoalComplete?.Invoke();
        }
    }
}
