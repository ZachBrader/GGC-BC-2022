using System;
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

    public event Action onQuestComplete;

    // Initialize quest
    public void Start()
    {
        isComplete = false;

        // Set up goals and subscribe
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
            Complete();
        }
    }

    public void Complete()
    {
        // Mark quest complete
        isComplete = true;

        // Invoke subscribers
        onQuestComplete?.Invoke();

        // Remove listerners
        foreach (Goal goal in goals)
        {
            goal.onGoalComplete -= CheckGoals;
        }
    }

    [System.Serializable]
    public abstract class Goal : MonoBehaviour 
    {
        public string description = "Default";
        public int currentAmount { get; protected set; }
        public int requiredAmount = 1;

        public bool isComplete;

        public event Action onGoalComplete;

        public virtual string GetDetails()
        {
            return description + " (" + currentAmount.ToString() + "/" + requiredAmount.ToString() + ")"; 
        }

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
