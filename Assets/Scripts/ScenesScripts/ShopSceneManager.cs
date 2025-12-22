using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// Controls the Shop scene UI and navigation.
public class ShopSceneManager : MonoBehaviour
{
    /// Reference to the UI text used for the "Continue" button label.
    [SerializeField] private TextMeshProUGUI nextLevelButtonText;

    /// Updates the next level button label to reflect current progression.
    void Start()
    {
        UpdateNextLevelButton();
    }

    /// Updates the "Continue" button label to display the scene returned by <see cref="GetNextLevelName"/>.
    private void UpdateNextLevelButton()
    {
        string nextLevel = GetNextLevelName();

        // Only update the UI if the reference is assigned.
        if (nextLevelButtonText != null)
        {
            nextLevelButtonText.text = $"Continue to {nextLevel}";
        }

        // Diagnostic logging for verification in the Console.
        Debug.Log($"Shop: Next level is {nextLevel}");
    }

    /// Loads the next level scene based on the player's saved progression.
    /// Intended to be connected to a UI button.
    public void LoadNextLevel()
    {
        string nextLevelName = GetNextLevelName();

        Debug.Log($"Shop: Loading next level: {nextLevelName}");
        SceneManager.LoadScene(nextLevelName);
    }

    /// Determines the next level scene name using <see cref="PlayerPrefs"/> key "LastCompletedLevel".
    /// <returns>The scene name to load next.</returns>
    private string GetNextLevelName()
    {
        // Read the most recently completed level; default 0 means the player has not completed any level yet.
        int lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0);
        int nextLevelNumber = lastCompletedLevel + 1;

        Debug.Log($"Shop: LastCompletedLevel = {lastCompletedLevel}, Next = {nextLevelNumber}");

        // Map the computed next level number to a scene name.
        switch (nextLevelNumber)
        {
            case 1:
                return "Level 1";
            case 2:
                return "Level 2";
            case 3:
                return "Level 3";
            case 4:
                return "MenuScene";
            default:
                return "MenuScene";
        }
    }

    /// Loads the main menu scene.
    /// Intended to be wired to a UI button.
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    /// Resets stored player progression by deleting relevant <see cref="PlayerPrefs"/> keys,
    /// then refreshes the shop UI to reflect the reset state.
    public void ResetProgression()
    {
        PlayerPrefs.DeleteKey("HighestCompletedLevel");
        PlayerPrefs.DeleteKey("LastCompletedLevel");
        PlayerPrefs.Save();
        Debug.Log("Player progression reset.");

        UpdateNextLevelButton();
    }
}