using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject xpPrefab;
    [SerializeField] GameObject coinPrefab;
    
    [SerializeField] int totalDrops = 2;
    [SerializeField] [Range(0f, 1f)] float coinDropChance = 0.5f;
    
    protected override void Die()
    {
        GameObject prefabToSpawn = Random.value < coinDropChance ? coinPrefab : xpPrefab;
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
