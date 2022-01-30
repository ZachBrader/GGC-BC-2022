using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuParent;
    private Credits credit;

    private void Start()
    {
        credit = GetComponent<Credits>();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        mainMenuParent.SetActive(false);
        credit.OpenCredits();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        credit.CloseCredits();
        mainMenuParent.SetActive(true);
    }
}
