using UnityEngine;

/// Spawns enemy instances at a fixed interval at this spawner's position.
public class EnemySpawner : MonoBehaviour
{
    /// Prefab to instantiate as an enemy.
    [SerializeField] GameObject Enemy;


    /// Time in seconds between enemy spawns.
    [SerializeField] float spawnInterval = 2f;

    /// Accumulates time to determine when to spawn the next enemy.
    private float timer = 0f;

    /// Increments the spawn timer and instantiates an enemy when the interval elapses.
    protected void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}