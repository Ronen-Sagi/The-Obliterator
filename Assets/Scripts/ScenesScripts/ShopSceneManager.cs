using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nextLevelButtonText; 
    
    void Start()
    {
        UpdateNextLevelButton();
    }
    
    private void UpdateNextLevelButton()
    {
        string nextLevel = GetNextLevelName();
        
        if (nextLevelButtonText != null)
        {
            nextLevelButtonText.text = $"Continue to {nextLevel}";
        }
        
        Debug.Log($"Shop: Next level is {nextLevel}");
    }
    
    public void LoadNextLevel()
    {
        string nextLevelName = GetNextLevelName();
        
        Debug.Log($"Shop: Loading next level: {nextLevelName}");
        SceneManager.LoadScene(nextLevelName);
    }

    private string GetNextLevelName()
    {
        int lastCompletedLevel = PlayerPrefs. GetInt("LastCompletedLevel", 0);
        int nextLevelNumber = lastCompletedLevel + 1;
        Debug.Log($"Shop: LastCompletedLevel = {lastCompletedLevel}, Next = {nextLevelNumber}");
        
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
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void ResetProgression()
    {
        PlayerPrefs.DeleteKey("HighestCompletedLevel");
        PlayerPrefs.DeleteKey("LastCompletedLevel");
        PlayerPrefs.Save();
        Debug.Log("Player progression reset.");
        
        UpdateNextLevelButton();
    }
}