using System.Collections;
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

    public List<Quest> quests;

    // Start is called before the first frame update
    void Start()
    {
        quests = new List<Quest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
