using UnityEngine;

/// Controls enemy movement by continuously moving toward the player.
public class EnemyMovement : MonoBehaviour
{
    /// Reference to the player GameObject found by the "Player" tag at runtime.
    private GameObject player;

    /// Movement speed in units per second at which the enemy approaches the player.
    [SerializeField] private float moveSpeed = 2f;

    /// Initializes the player reference by finding the GameObject tagged as "Player".
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// Calculates a normalized direction vector toward the player and moves the enemy accordingly.
    void Update()
    {
        if (player == null) return;

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
