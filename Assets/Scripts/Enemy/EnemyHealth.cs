using UnityEngine;

/// Health component for enemy entities.
/// Spawns a drop on death (either XP or a coin) based on a configurable chance,
/// then destroys the enemy GameObject.
public class EnemyHealth : Health
{
    /// Prefab spawned when the enemy drops XP.
    [SerializeField] GameObject xpPrefab;

    /// Prefab spawned when the enemy drops a coin.
    [SerializeField] GameObject coinPrefab;

    /// Intended number of total drops.
    /// Note: this field is currently unused by <see cref="Die"/> (only one drop is spawned).
    [SerializeField] int totalDrops = 2;

    /// Probability that the enemy will drop a coin instead of XP.
    [SerializeField] [Range(0f, 1f)] float coinDropChance = 0.5f;

    /// Called when the enemy dies.
    /// Randomly chooses a drop prefab (coin or XP) using <see cref="coinDropChance"/>,
    /// instantiates it at the enemy's position, then destroys the enemy GameObject.
    protected override void Die()
    {
        GameObject prefabToSpawn = Random.value < coinDropChance ? coinPrefab : xpPrefab;
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}