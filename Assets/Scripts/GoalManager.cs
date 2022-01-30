using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    #region Singleton
    public static GoalManager instance;

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

    private GameManager gameManager;
    public List<DogCage> dogCages;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    private void Update()
    {
        if (dogCages.Count == 0)
        {
            gameManager.CompleteLevel();
        }
    }
}
