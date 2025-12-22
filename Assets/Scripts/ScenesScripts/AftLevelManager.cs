using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// Manages the "after level" (post\-\-completion) UI and navigation flow.
public class AftLevelManager : MonoBehaviour
{
    /// UI text element used to display the completion title.
    [SerializeField] private TextMeshProUGUI titleText;

    /// UI text element used to label the "next level" button.
    [SerializeField] private TextMeshProUGUI nextLevelButtonText;

    /// The last level index that was completed, loaded from <see cref="PlayerPrefs"/>.
    private int lastCompletedLevel;

    /// The scene name determined as the next destination after completing <see cref="lastCompletedLevel"/>.
    private string nextLevelSceneName;


    /// Initializes level state, updates UI, and logs the navigation outcome.
    void Start()
    {
        // Retrieve last completed level from persistent storage.
        // Key: "LastCompletedLevel", Default: 1
        lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 1);

        // Determine the next scene to load based on the completed level.
        nextLevelSceneName = GetNextLevelName(lastCompletedLevel);

        // Update the title text if assigned in the Inspector.
        if (titleText != null)
        {
            titleText.text = $"Level {lastCompletedLevel} Complete!";
        }

        // Update the button label if assigned in the Inspector.
        if (nextLevelButtonText != null)
        {
            nextLevelButtonText.text = $"Continue to {nextLevelSceneName}";
        }

        // Diagnostic logging for verification in the Console.
        Debug.Log($"Completed Level {lastCompletedLevel}.  Next:  {nextLevelSceneName}");
    }

    /// Loads the next scene determined from <see cref="GetNextLevelName(int)"/>.
    /// Intended to be connected to a UI button.
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
    }

    /// Loads the shop scene.
    /// Intended to be wired to a UI button.
    public void LoadShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    /// Maps a completed level index to the next scene name.
    /// <param name="completedLevel">The level index that was just completed.</param>
    /// <returns>The scene name to load next.</returns>
    private string GetNextLevelName(int completedLevel)
    {
        switch (completedLevel)
        {
            case 1:
                return "Level 2";
            case 2:
                return "Level 3";
            case 3:
                return "MenuScene";
            default:
                return "MenuScene";
        }
    }

    /// Loads the main menu scene.
    /// Intended to be connected to a UI button.
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}