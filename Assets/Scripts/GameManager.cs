using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Instances Detected");
            return;
        }
        instance = this;
        Debug.Log("Assigned Game Manager to instance");
    }
    #endregion

    // Public
    public float restartDelay = 2f; // Delay before restarting game
    public GameObject completeLevelUI; // Reference to UI element for selecting next level
    
    // Private
    private bool gameHasEnded = false; // Keeps track of if the game has ended

    // Logic for when player finishes level
    public void CompleteLevel() 
    {
        Debug.Log("Level over!");
        // completeLevelUI.SetActive(true);
    }

    // Restarts current level
    void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // Player has died
    public void EndGame() 
    {
        if (gameHasEnded == false) // Locks function so game is not restarted multiple times
        {
            gameHasEnded = true;
            Debug.Log("End Game");
            Invoke("Restart", restartDelay); // Restart game after a few seconds
        }
    }
}