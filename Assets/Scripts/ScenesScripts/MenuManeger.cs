using UnityEngine;
using UnityEngine.SceneManagement;

/// Handles main menu navigation by loading the appropriate scenes.
public class MenuManager : MonoBehaviour
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
    
    public void LoadShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}