using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private float levelTime = 60f; // Level time in seconds
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private string nextSceneName;
    [SerializeField] private int levelNumber = 0;

    private float timeRemaining;
    private bool timerIsRunning = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerIsRunning = false;
                OnTimerComplete();
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerComplete()
    {
        // Save which level was just completed
        PlayerPrefs.SetInt("LastCompletedLevel", levelNumber);
        
        // Save progression - this level completed
        int currentHighest = PlayerPrefs.GetInt("HighestCompletedLevel", 0);
        if (levelNumber > currentHighest)
        {
            PlayerPrefs.SetInt("HighestCompletedLevel", levelNumber);
        }
        PlayerPrefs.Save();
        Debug.Log($"Level {levelNumber} completed! Saved progression.");
        
        // Load the generic after-level scene
        SceneManager.LoadScene("AftLevel");
    }
}