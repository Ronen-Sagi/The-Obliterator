using UnityEngine;
using UnityEngine.SceneManagement;

/// Scene navigation helper for a UI with three buttons.
public class ThreeButtonsManager : MonoBehaviour
{
    /// Scene name to load when the upper button is pressed.
    [SerializeField] private string upperButtonSceneName;

    /// Scene name to load when the middle button is pressed.
    [SerializeField] private string middleButtonSceneName;

    /// Scene name to load when the lower button is pressed.
    [SerializeField] private string lowerButtonSceneName;

    /// Loads the scene configured by <see cref="upperButtonSceneName"/>.
    public void LoadupperButton()
    {
        SceneManager.LoadScene(upperButtonSceneName);
    }

    /// Loads the scene configured by <see cref="middleButtonSceneName"/>.
    public void LoadMiddleButton()
    {
        SceneManager.LoadScene(middleButtonSceneName);
    }

    /// Loads the scene configured by <see cref="lowerButtonSceneName"/>.
    public void LoadlowerButton()
    {
        SceneManager.LoadScene(lowerButtonSceneName);
    }
}