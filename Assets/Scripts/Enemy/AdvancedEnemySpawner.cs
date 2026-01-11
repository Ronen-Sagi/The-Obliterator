using UnityEngine;
using System.Collections.Generic;

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
    
    [Header("Wall Buffer Zone")]
    [SerializeField] private float wallBuffer = 1.5f;
    
    [Header("Player Buffer Zone")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float bufferRadius = 3f;
    
    [Header("Enemy Separation")]
    [SerializeField] private float minEnemyDistance = 1.0f; // Minimum distance between spawned enemies

    [Header("Enemy Type Progression (in seconds)")]
    [SerializeField] private float fastEnemyStartTime = 30f;
    [SerializeField] private float strongEnemyStartTime = 60f;
    [SerializeField] private float basicEnemyEndTime = 45f;

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
            Debug. LogWarning("No valid enemy type to spawn at this time!");
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

        if (basicEnemyPrefab != null && (basicEnemyEndTime < 0 || elapsedTime < basicEnemyEndTime))
        {
            availableEnemies.Add(basicEnemyPrefab);
            weights.Add(basicEnemyWeight);
        }

        if (fastEnemyPrefab != null && elapsedTime >= fastEnemyStartTime)
        {
            availableEnemies.Add(fastEnemyPrefab);
            weights.Add(fastEnemyWeight);
        }

        if (strongEnemyPrefab != null && elapsedTime >= strongEnemyStartTime)
        {
            availableEnemies.Add(strongEnemyPrefab);
            weights.Add(strongEnemyWeight);
        }

        if (availableEnemies.Count == 0)
            return null;

        return GetWeightedRandomEnemy(availableEnemies, weights);
    }

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

    /// Gets a random spawn position within bounds, avoiding walls, player, and other enemies
    Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomPosition;
        int maxAttempts = 30;
        int attempts = 0;

        // Calculate safe spawn area (away from walls)
        Vector2 safeMin = spawnAreaMin + Vector2.one * wallBuffer;
        Vector2 safeMax = spawnAreaMax - Vector2.one * wallBuffer;

        do
        {
            float x = Random.Range(safeMin.x, safeMax.x);
            float y = Random.Range(safeMin.y, safeMax. y);
            randomPosition = new Vector2(x, y);
            attempts++;

            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Could not find valid spawn position after max attempts!");
                break;
            }

        } while (IsPositionTooCloseToPlayer(randomPosition) || 
                 IsPositionInsideWall(randomPosition) || 
                 IsPositionTooCloseToEnemy(randomPosition));

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

    /// Checks if a position overlaps with a wall or obstacle
    bool IsPositionInsideWall(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, 0.5f);
        
        if (hit != null && hit.CompareTag("Wall"))
        {
            return true;
        }
        
        return false;
    }

    /// Checks if a position is too close to an existing enemy
    bool IsPositionTooCloseToEnemy(Vector2 position)
    {
        // Check for any enemies within the minimum distance
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(position, minEnemyDistance);
        
        foreach (Collider2D collider in nearbyColliders)
        {
            // If we find an enemy tag, position is too close
            if (collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        
        return false;
    }

    /// Visualizes spawn area and buffer zones in the editor
    void OnDrawGizmosSelected()
    {
        // Draw original spawn area (red)
        Gizmos.color = Color. red;
        Vector3 center = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2f, (spawnAreaMin.y + spawnAreaMax.y) / 2f, 0f);
        Vector3 size = new Vector3(spawnAreaMax. x - spawnAreaMin.x, spawnAreaMax.y - spawnAreaMin.y, 0f);
        Gizmos.DrawWireCube(center, size);

        // Draw safe spawn area (green) - away from walls
        Gizmos.color = Color.green;
        Vector2 safeMin = spawnAreaMin + Vector2.one * wallBuffer;
        Vector2 safeMax = spawnAreaMax - Vector2.one * wallBuffer;
        Vector3 safeCenter = new Vector3((safeMin.x + safeMax.x) / 2f, (safeMin.y + safeMax.y) / 2f, 0f);
        Vector3 safeSize = new Vector3(safeMax. x - safeMin.x, safeMax.y - safeMin.y, 0f);
        Gizmos.DrawWireCube(safeCenter, safeSize);

        // Draw player buffer zone (yellow)
        if (playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos. DrawWireSphere(playerTransform.position, bufferRadius);
        }
    }
}