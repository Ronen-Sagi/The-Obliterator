using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }


    public void LoadNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}