using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoButtonsManager : MonoBehaviour
{
    
    [SerializeField] private string upperButtonSceneName;
    [SerializeField] private string lowerButtonSceneName;
    
    /// Loads the main game scene.
    public void LoadupperButton()
    {
        SceneManager.LoadScene(upperButtonSceneName);
    }

    /// Loads the shop scene.
    public void LoadlowerButton()
    {
        SceneManager.LoadScene(lowerButtonSceneName);
    }
}