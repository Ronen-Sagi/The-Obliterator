using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// Coordinates the tutorial flow.
public class TutorialManager : MonoBehaviour
{
    /// UI text element displaying tutorial instructions.
    [SerializeField] private TextMeshProUGUI instructionText;

    /// Ordered list of tutorial dots the player must touch in sequence.
    [SerializeField] private List<GameObject> tutorialDots = new List<GameObject>();

    /// Ordered list of tutorial enemies activated step by step.
    [SerializeField] private List<GameObject> tutorialEnemies = new List<GameObject>();

    /// Ordered instruction strings corresponding to tutorial steps.
    [SerializeField] private List<string> instructions = new List<string>();

    /// Input action for the space key used to trigger the power-up.
    [SerializeField] private InputAction spaceKey = new InputAction(type: InputActionType.Button);

    /// Prefab for spawning enemies after the initial tutorial.
    [SerializeField] private GameObject enemyPrefab;

    /// Spawn points for enemies in the post-tutorial phase.
    [SerializeField] private List<Transform> enemySpawnPositions = new List<Transform>();

    /// Reference to the kill counter that tracks enemy kills in the post-tutorial phase.
    [SerializeField] private TutorialKillCounter killCounter;

    /// Index of the next dot the player should touch.
    private int currentDotIndex = 0;

    /// Index of the current tutorial enemy.
    private int currentEnemyIndex = 0;

    /// Overall tutorial step index.
    private int currentStep = 0;

    /// Total number of tutorial dots.
    private int totalDots = 0;

    /// Total number of tutorial enemies.
    private int totalEnemies = 0;

    /// Indicates whether the power-up can currently be activated.
    private bool powerUpAvaliable = false;

    /// Indicates whether the power-up has already been used.
    private bool powerUpUsed = false;

    /// Initializes counts, enables input, sets the first dot active, and displays the first instruction.
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

    /// Called when a tutorial dot is touched. Advances to the next dot if correct,
    /// otherwise starts the enemy tutorial section.
    /// <param name="dotNumber">The 1-based index of the dot that was touched.</param>
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

    /// Begins the enemy tutorial by activating the first enemy and updating instructions.
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
    }

    /// Handles logic when a tutorial enemy is destroyed,
    /// activates the power-up phase.
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
            powerUpAvaliable = true;
            instructionText.text =
                "Now let's activate your power up\\n Press the space button to activate your speed boost";
        }
    }

    /// Monitors input each frame to trigger the power-up when available and unused.
    private void Update()
    {
        if (powerUpAvaliable && !powerUpUsed && spaceKey.WasPerformedThisFrame())
        {
            powerUpUsed = true;
            ActivateSpeedPowerUp();
        }
    }

    /// Finds the player movement component and activates a temporary speed boost,
    /// then schedules follow-up messaging.
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

    /// Shows the next tutorial message after the speed boost delay and schedules enemy spawning.
    private void ShowDelayedMassage()
    {
        instructionText.text = "Now let's see how you deal with real enemies";
        Invoke(nameof(SpawnEnemies), 5f);
    }

    /// Spawns enemies for the kill moving enemies phase and activates the kill counter.
    private void SpawnEnemies()
    {
        Instantiate(enemyPrefab);

        instructionText.text = "Kill 10 enemies to complete the tutorial! ";

        if (killCounter != null)
        {
            killCounter.ActivateCounter();
        }
    }

    /// Updates the UI with the current kill count and transitions to the menu when the goal is reached.
    /// <param name="count">The number of enemies killed.</param>
    public void UpdateKillCount(int count)
    {
        instructionText.text = $"Enemies killed:  {count}/10";

        if (count >= 10)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    /// Disables the space key input action when the component is deactivated.
    private void OnDisable()
    {
        spaceKey.Disable();
    }
}