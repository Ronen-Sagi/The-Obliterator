using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AftLevelManager :  MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText; 
    [SerializeField] private TextMeshProUGUI nextLevelButtonText;
    
    private int lastCompletedLevel;
    private string nextLevelSceneName;
    
    void Start()
    {
        lastCompletedLevel = PlayerPrefs. GetInt("LastCompletedLevel", 1);
        nextLevelSceneName = GetNextLevelName(lastCompletedLevel);
        if (titleText != null)
        {
            titleText.text = $"Level {lastCompletedLevel} Complete!";
        }
        
        if (nextLevelButtonText != null)
        {
            nextLevelButtonText.text = $"Continue to {nextLevelSceneName}";
        }
        
        Debug.Log($"Completed Level {lastCompletedLevel}.  Next:  {nextLevelSceneName}");
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
    }
    
    
    public void LoadShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
    

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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}