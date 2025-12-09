using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] float spawnInterval = 2f;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
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