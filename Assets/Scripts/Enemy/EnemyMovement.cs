using UnityEngine;

/// Controls enemy movement by continuously moving toward the player.
public class EnemyMovement : MonoBehaviour
{
    /// Reference to the player GameObject found by the "Player" tag at runtime.
    private GameObject player;
    private EnemyStats es;

    /// Initializes the player reference by finding the GameObject tagged as "Player".
    void Awake()
    {
        es = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// Calculates a normalized direction vector toward the player and moves the enemy accordingly.
    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position += (Vector3)direction * es.MoveSpeed * Time.deltaTime;
    }
}