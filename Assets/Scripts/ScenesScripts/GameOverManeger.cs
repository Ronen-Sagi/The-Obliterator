using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManeger : MonoBehaviour
{
    /// Loads the main game scene.
    public void LoadNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// Loads the shop scene.
    public void LoadShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}