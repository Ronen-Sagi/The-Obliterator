using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages player experience, leveling up, and triggering mutations.
/// </summary>
public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float currentXP = 0;
    [SerializeField] private float xpToNextLevel = 100;
    [SerializeField] private float xpGrowthFactor = 1.2f;

    // Mutation Options (Simple strings or Enums for now, later ScriptableObjects)
    private List<string> availableMutations = new List<string>
    {
        "Bullets Explode",
        "Leave Fire Trail",
        "Heal on Kill",
        "Increase Fire Rate",
        "Increase Move Speed"
    };

    // Events
    public delegate void LevelUpAction(List<string> options);
    public static event LevelUpAction OnLevelUp;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void AddXP(int amount)
    {
        float multiplier = 1f;
        if (PowerUpManager.Instance != null && PowerUpManager.Instance.IsXPSurgeActive)
        {
            multiplier = PowerUpManager.Instance.XPSurgeMultiplier;
        }

        currentXP += amount * multiplier;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        xpToNextLevel *= xpGrowthFactor;

        // Notify EnemySpawner to scale difficulty
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>(); // Changed from FindFirstObjectByType for compatibility
        if (spawner != null)
        {
            spawner.SetLevel(currentLevel);
        }

        // Trigger Evolution Menu
        List<string> options = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            options.Add(availableMutations[Random.Range(0, availableMutations.Count)]);
        }

        Debug.Log("Level Up! Choose a mutation: " + string.Join(", ", options));

        if (OnLevelUp != null)
        {
             Time.timeScale = 0f; // Pause game
             OnLevelUp.Invoke(options);
        }
        else
        {
            // No UI listener, auto-select first option to keep game running
            Debug.LogWarning("No UI listener for Level Up. Auto-selecting first option.");
            SelectMutation(options[0]);
        }
    }

    // Called by UI when a mutation is selected
    public void SelectMutation(string mutation)
    {
        ApplyMutation(mutation);
        Time.timeScale = 1f; // Resume game
    }

    private void ApplyMutation(string mutation)
    {
        Debug.Log("Applying mutation: " + mutation);
        // Implement mutation logic
        // We need references to Player components
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        switch (mutation)
        {
            case "Heal on Kill":
                // Add HealOnKill component or flag
                // We'd need to modify EnemyHealth to notify player on death
                break;
            case "Increase Fire Rate":
                 // Modify base stats
                 break;
            case "Increase Move Speed":
                 // Modify movement
                 break;
            // etc
        }
    }
}
