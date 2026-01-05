using UnityEngine;
using System.Collections. Generic;

/// Advanced enemy spawner that spawns enemies at random positions with progressive difficulty
public class AdvancedEnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;
    [SerializeField] private GameObject strongEnemyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-14.5f, -14f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(14.5f, 14f);
    
    [Header("Player Buffer Zone")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float bufferRadius = 3f; // Enemies won't spawn within this distance from player

    [Header("Enemy Type Progression (in seconds)")]
    [SerializeField] private float fastEnemyStartTime = 30f;  // When FastEnemy starts spawning
    [SerializeField] private float strongEnemyStartTime = 60f; // When StrongEnemy starts spawning
    [SerializeField] private float basicEnemyEndTime = 45f;    // When BasicEnemy stops spawning (-1 = never stops)

    [Header("Enemy Type Weights (0-100)")]
    [SerializeField] private float basicEnemyWeight = 100f;
    [SerializeField] private float fastEnemyWeight = 50f;
    [SerializeField] private float strongEnemyWeight = 30f;

    private float timer = 0f;
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomEnemy();
            timer = 0f;
        }
    }

    /// Spawns a random enemy at a random valid position
    void SpawnRandomEnemy()
    {
        GameObject enemyToSpawn = ChooseEnemyType();
        
        if (enemyToSpawn == null)
        {
            Debug.LogWarning("No valid enemy type to spawn at this time!");
            return;
        }

        Vector2 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyToSpawn, spawnPosition, Quaternion. identity);
    }

    /// Chooses which enemy type to spawn based on elapsed time and weights
    GameObject ChooseEnemyType()
    {
        List<GameObject> availableEnemies = new List<GameObject>();
        List<float> weights = new List<float>();

        // Check if BasicEnemy is available
        if (basicEnemyPrefab != null && (basicEnemyEndTime < 0 || elapsedTime < basicEnemyEndTime))
        {
            availableEnemies.Add(basicEnemyPrefab);
            weights. Add(basicEnemyWeight);
        }

        // Check if FastEnemy is available
        if (fastEnemyPrefab != null && elapsedTime >= fastEnemyStartTime)
        {
            availableEnemies.Add(fastEnemyPrefab);
            weights.Add(fastEnemyWeight);
        }

        // Check if StrongEnemy is available
        if (strongEnemyPrefab != null && elapsedTime >= strongEnemyStartTime)
        {
            availableEnemies.Add(strongEnemyPrefab);
            weights.Add(strongEnemyWeight);
        }

        if (availableEnemies.Count == 0)
            return null;

        // Weighted random selection
        return GetWeightedRandomEnemy(availableEnemies, weights);
    }

    /// Returns a random enemy based on weights
    GameObject GetWeightedRandomEnemy(List<GameObject> enemies, List<float> weights)
    {
        float totalWeight = 0f;
        foreach (float weight in weights)
            totalWeight += weight;

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        for (int i = 0; i < enemies.Count; i++)
        {
            cumulative += weights[i];
            if (randomValue <= cumulative)
                return enemies[i];
        }

        return enemies[enemies.Count - 1];
    }

    /// Gets a random spawn position within bounds, avoiding the player buffer zone
    Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomPosition;
        int maxAttempts = 30;
        int attempts = 0;

        do
        {
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            randomPosition = new Vector2(x, y);
            attempts++;

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Could not find valid spawn position after max attempts!");
                break;
            }

        } while (IsPositionTooCloseToPlayer(randomPosition));

        return randomPosition;
    }

    /// Checks if a position is within the player buffer zone
    bool IsPositionTooCloseToPlayer(Vector2 position)
    {
        if (playerTransform == null)
            return false;

        float distance = Vector2.Distance(position, playerTransform.position);
        return distance < bufferRadius;
    }

    /// Visualizes spawn area and buffer zone in the editor
    void OnDrawGizmosSelected()
    {
        // Draw spawn area
        Gizmos. color = Color.green;
        Vector3 center = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2f, (spawnAreaMin.y + spawnAreaMax.y) / 2f, 0f);
        Vector3 size = new Vector3(spawnAreaMax.x - spawnAreaMin.x, spawnAreaMax. y - spawnAreaMin.y, 0f);
        Gizmos.DrawWireCube(center, size);

        // Draw player buffer zone
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, bufferRadius);
        }
    }
}