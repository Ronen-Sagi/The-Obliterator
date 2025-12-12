using UnityEngine;
using UnityEngine.SceneManagement;

public class RepeatTutorial : MonoBehaviour
{
    /// Loads the tutorial scene.
    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    /// Loads the main game scene.
    public void LoadNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
