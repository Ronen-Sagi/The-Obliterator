using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private List<GameObject> tutorialDots = new List<GameObject>();
    [SerializeField] private List<GameObject> tutorialEnemies = new List<GameObject>();
    [SerializeField] private List<string> instructions = new List<string>();
    
    private int currentDotIndex = 0;
    private int currentEnemyIndex = 0;
    private int currentStep = 0;
    private int totalDots = 0;
    private int totalEnemies = 0;

    void Start()
    {
        totalDots = tutorialDots.Count;
        totalEnemies = tutorialEnemies.Count;
        
        for (int i = 0; i < totalDots; i++)
        {
            tutorialDots[i].SetActive(i == 0);
        }
        
        foreach (GameObject enemy in tutorialEnemies)
        {
            enemy.SetActive(false);
        }
        
        if (instructions.Count > 0)
        {
            instructionText.text = instructions[0];
        }
    }
    
    public void OnDotTouched(int dotNumber)
    {
        if (dotNumber == currentDotIndex + 1 && currentStep == currentDotIndex)
        {
            currentDotIndex++;
            currentStep++;
            
            if (currentDotIndex < totalDots)
            {
                tutorialDots[currentDotIndex].SetActive(true);
                
                if (currentStep < instructions.Count)
                {
                    instructionText. text = instructions[currentStep];
                }
            }
            else
            {
                StartEnemyTutorial();
            }
        }
    }

    private void StartEnemyTutorial()
    {
        if (totalEnemies > 0)
        {
            tutorialEnemies[0].SetActive(true);
            
            int instructionIndex = totalDots + currentEnemyIndex;
            if (instructionIndex < instructions.Count)
            {
                instructionText.text = instructions[instructionIndex];
            }
        }
        else
        {
            CompleteTutorial();
        }
    }
    
    public void OnEnemyDestroyed()
    {
        currentEnemyIndex++;
        currentStep++;
        
        if (currentEnemyIndex < totalEnemies)
        {
            tutorialEnemies[currentEnemyIndex].SetActive(true);
            
            int instructionIndex = totalDots + currentEnemyIndex;
            if (instructionIndex < instructions. Count)
            {
                instructionText.text = instructions[instructionIndex];
            }
        }
        else
        {
            CompleteTutorial();
        }
    }

    private void CompleteTutorial()
    {
        if (instructions.Count > totalDots + totalEnemies)
        {
            instructionText.text = instructions[totalDots + totalEnemies];
        }
        else
        {
            instructionText.text = "Tutorial complete\n";
        }

        //LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}