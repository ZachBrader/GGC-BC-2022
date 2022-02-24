/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;

[System.Serializable]
public class QuestOld : ScriptableObject
{
    public bool isComplete { get; protected set; } = false;
    public bool isPrimary = false;

    
    public List<Goal> goals; // List of goals to complete quests

    public delegate void OnQuestComplete();
    public OnQuestComplete onQuestComplete;

    public QuestCompletedEvent questCompleted;

    [System.Serializable]
    public struct Stat
    {
        public int currencies;
        public int xp;
        public string test;
    }

    // Stores information on the quest
    [System.Serializable]
    public struct Info
    {
        public string name;
        public string description;
    }

    

    public Info information = new Info { name="Quest", description="Do the thing" };

    // Stores rewards the quest will give
    public Stat reward = new Stat { currencies = 10, xp = 10 };

    [System.Serializable]
    public abstract class Goal : ScriptableObject
    {
        protected string description { get; set; } = "Default";
        public int currentAmount { get; protected set; }
        public int requiredAmount = 1;

        public bool isCompleted;

        public delegate void OnGoalComplete();
        public OnGoalComplete onGoalComplete;

        [HideInInspector] public UnityEvent goalCompleted;

        

        public virtual void Initialize()
        {
            isCompleted = false;
            currentAmount = 0;

            goalCompleted = new UnityEvent();
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
            isCompleted = true;
            goalCompleted.Invoke();
            goalCompleted.RemoveAllListeners();
        }
    }

    // Initialize quest
    public void Initialize()
    {
        goals = new List<Goal>();
        isComplete = false;

        foreach (Goal goal in goals)
        {
            goal.Initialize();
            goal.onGoalComplete += CheckGoals;
            goal.goalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    // Evaluate if quest is finished
    public void CheckGoals()
    {
        bool goalCheck = true;
        foreach (Goal goal in goals)
        {
            if (!goal.isCompleted)
            {
                goalCheck = false;
            }
        }

        if (goalCheck)
        {
            isComplete = true;

            questCompleted.Invoke(this);
            questCompleted.RemoveAllListeners();

            // Remove listerners
        }
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> { }

// Quest Editor logic
#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    SerializedProperty m_QuestInfoProperty;
    SerializedProperty m_QuestStatProperty;

    List<string> m_QuestGoalType;
    SerializedProperty m_QuestGoalListProperty;

    [MenuItem("Assets/Quest", priority = 0)]
    public static void CreateQuest()
    {
        var newQuest = CreateInstance<Quest>();

        ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
    }

    private void OnEnable()
    {
        m_QuestInfoProperty = serializedObject.FindProperty(nameof(Quest.information));
        m_QuestStatProperty = serializedObject.FindProperty(nameof(Quest.reward));

        m_QuestGoalListProperty = serializedObject.FindProperty(nameof(Quest.goals));

        var lookup = typeof(Quest.Goal);
        m_QuestGoalType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        var child = m_QuestInfoProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        /*child = m_QuestStatProperty.Copy();
        depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest Reward", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
            
        }

        EditorGUILayout.LabelField("Goals", EditorStyles.boldLabel);

        int choice = EditorGUILayout.Popup("Add New Goal", -1, m_QuestGoalType.ToArray());
        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_QuestGoalType[choice]);

            AssetDatabase.AddObjectToAsset(newInstance, target);
            m_QuestGoalListProperty.InsertArrayElementAtIndex(m_QuestGoalListProperty.arraySize);
            m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1)
                .objectReferenceValue = newInstance;
        }

        Editor ed = null;

        int toDelete = -1;
        for (int i = 0; i < m_QuestGoalListProperty.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

            ed.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);

            m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
            m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        serializedObject.ApplyModifiedProperties();
    } 
}
#endif
*/