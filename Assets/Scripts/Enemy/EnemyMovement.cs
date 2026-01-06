using UnityEngine;

/// Controls enemy movement by continuously moving toward the player.
public class EnemyMovement : MonoBehaviour
{
    /// Reference to the player GameObject found by the "Player" tag at runtime.
    private GameObject player;
    private EnemyStats es;

    /// Smooth rotation speed (higher = faster rotation)
    [SerializeField] private float rotationSpeed = 10f;

    /// Initializes the player reference by finding the GameObject tagged as "Player".
    void Awake()
    {
        es = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// Calculates a normalized direction vector toward the player and moves the enemy accordingly.
    void Update()
    {
        Vector2 direction = player.transform. position - transform.position;
        direction.Normalize();
        
        // Move the enemy
        transform.position += (Vector3)direction * es.MoveSpeed * Time.deltaTime;
        
        // Calculate the angle in degrees
        float angle = Mathf. Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Apply rotation - adjust the -90f offset based on your sprite's default facing direction
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        
        // Smooth rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}