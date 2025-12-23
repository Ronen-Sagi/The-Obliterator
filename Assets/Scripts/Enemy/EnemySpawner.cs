using UnityEngine;
using System.Collections.Generic;

/// Spawns enemy instances with scaling stats based on level.
public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemySpawnConfig
    {
        public GameObject prefab;
        public EnemyConfig config;
        public float spawnWeight; // Chance to spawn this type relative to others
    }

    [SerializeField] private List<EnemySpawnConfig> enemyTypes;

    /// Fallback prefab if list is empty (legacy support)
    [SerializeField] private GameObject defaultEnemyPrefab;

    /// Time in seconds between enemy spawns.
    [SerializeField] float spawnInterval = 2f;

    /// Current level for scaling.
    [SerializeField] private int currentLevel = 1;

    private float timer = 0f;

    protected void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = defaultEnemyPrefab;
        EnemyConfig configToApply = null;

        if (enemyTypes != null && enemyTypes.Count > 0)
        {
            float totalWeight = 0f;
            foreach (var et in enemyTypes) totalWeight += et.spawnWeight;

            float randomValue = Random.Range(0f, totalWeight);
            float currentWeight = 0f;

            foreach (var et in enemyTypes)
            {
                currentWeight += et.spawnWeight;
                if (randomValue <= currentWeight)
                {
                    prefabToSpawn = et.prefab;
                    configToApply = et.config;
                    break;
                }
            }
        }

        if (prefabToSpawn != null)
        {
            GameObject enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            ApplyScaling(enemy, configToApply);
        }
    }

    private void ApplyScaling(GameObject enemy, EnemyConfig config)
    {
        // Calculate scaling multiplier: 1.15^(Level - 1)
        float multiplier = Mathf.Pow(1.15f, currentLevel - 1);

        // Apply Health
        EnemyHealth healthComp = enemy.GetComponent<EnemyHealth>();
        if (healthComp != null)
        {
            float baseHealth = config != null ? config.baseHealth : 10f; // Default if no config
            float scaledHealth = baseHealth * multiplier;
            healthComp.SetMaxHealth(scaledHealth);
        }

        // Apply Speed
        EnemyMovement movementComp = enemy.GetComponent<EnemyMovement>();
        if (movementComp != null)
        {
            float baseSpeed = config != null ? config.baseSpeed : 2f;
            movementComp.SetSpeed(baseSpeed);
        }

        // Apply Damage
        DamageDealer damageComp = enemy.GetComponent<DamageDealer>();
        if (damageComp != null)
        {
            float baseDamage = config != null ? config.baseDamage : 10f;
            float scaledDamage = baseDamage * multiplier;
            damageComp.SetDamage(scaledDamage);
        }
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
    }
}
