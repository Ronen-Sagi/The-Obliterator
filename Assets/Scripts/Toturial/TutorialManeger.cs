using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private List<GameObject> tutorialDots = new List<GameObject>();
    [SerializeField] private List<GameObject> tutorialEnemies = new List<GameObject>();
    [SerializeField] private List<string> instructions = new List<string>();
    [SerializeField] private InputAction spaceKey = new InputAction(type: InputActionType.Button);
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> enemySpawnPositions = new List<Transform>();
    
    private int currentDotIndex = 0;
    private int currentEnemyIndex = 0;
    private int currentStep = 0;
    private int totalDots = 0;
    private int totalEnemies = 0;
    private bool powerUpAvaliable = false;
    private bool powerUpUsed = false;

    void Start()
    {
        totalDots = tutorialDots.Count;
        totalEnemies = tutorialEnemies.Count;
        
        spaceKey.AddBinding("<Keyboard>/space");
        spaceKey.Enable();
        
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
        Debug.Log($"Dot touched: {dotNumber}, currentDotIndex: {currentDotIndex}");
        if (dotNumber == currentDotIndex + 1)
        {
            currentDotIndex++;
            currentStep++;
            if (currentDotIndex < totalDots)
            {
                tutorialDots[currentDotIndex].SetActive(true);
                if (currentStep < instructions.Count)
                {
                    instructionText.text = instructions[currentStep];
                }
                Debug.Log($"Next dot activated: {currentDotIndex}");
            }
        }
        else
        {
            StartEnemyTutorial();
        }
    }
    private void StartEnemyTutorial()
    {
        if (totalEnemies > 0)
        {
            tutorialEnemies[0].SetActive(true);
            
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
    public void OnEnemyDestroyed()
    {
        currentEnemyIndex++;
        currentStep++;
        int instructionIndex = totalDots;
        
        if (currentEnemyIndex < totalEnemies)
        {
            tutorialEnemies[currentEnemyIndex].SetActive(true);
            
            instructionIndex += currentEnemyIndex;
            if (instructionIndex < instructions.Count)
            {
                instructionText.text = instructions[instructionIndex];
            }
        }
        else
        {
            //CompleteTutorial();
            powerUpAvaliable = true;
            instructionText.text = "Now let's activate your power up\\n Press the space button to activate your speed boost";
        }
    }

    private void Update()
    {
        if (powerUpAvaliable && !powerUpUsed && spaceKey.WasPerformedThisFrame())
        {
            powerUpUsed = true;
            ActivateSpeedPowerUp();
        }
    }

    private void ActivateSpeedPowerUp()
    {
        Movment playerMovement = FindObjectOfType<Movment>();
        if (playerMovement != null)
        {
            playerMovement.ActivateSpeedBoost(5f);
            instructionText.text = "Speed boost activated!";
            Invoke(nameof(ShowDelayedMassage), 8f);
        }
    }

    private void ShowDelayedMassage()
    {
        instructionText.text = "Now let's see how you deal with real enemies";
        Invoke(nameof(SpawnEnemies), 5f);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 spawnPosition;
            
            // If spawn positions are defined, use them (cycling through if more enemies than positions)
            if (enemySpawnPositions.Count > 0)
            {
                spawnPosition = enemySpawnPositions[i % enemySpawnPositions.Count]. position;
            }
            else
            {
                // Otherwise spawn randomly around the edges of the screen
                spawnPosition = GetRandomSpawnPosition();
            }
            
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        
        instructionText.text = "Survive! ";
        
        // Complete tutorial after some time
        Invoke(nameof(CompleteTutorial), 10f);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Camera mainCam = Camera.main;
        float spawnDistance = 12f; 
        
        float angle = Random.Range(0f, 360f);
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spawnDistance;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * spawnDistance;
        
        return new Vector3(x, y, 0);
    
    }

    private void CompleteTutorial()
    {
        if (instructions.Count > totalDots + totalEnemies)
        {
            instructionText.text = instructions[totalDots + totalEnemies];
        }
        else
        {
            instructionText.text = "Tutorial complete!\n";
        }

        //LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager. LoadScene("MainScene");
    }
    
    private void OnDisable()
    {
        spaceKey.Disable();
    }
}