using UnityEngine;

/// Controls enemy movement by continuously moving toward the player. 
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement :  MonoBehaviour
{
    private GameObject player;
    private EnemyStats es;
    private Rigidbody2D rb;

    /// Smooth rotation speed (higher = faster rotation)
    [SerializeField] private float rotationSpeed = 10f;

    void Awake()
    {
        es = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject. FindGameObjectWithTag("Player");
    }

    void FixedUpdate() // Use FixedUpdate for physics! 
    {
        if (player == null) return;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        
        // Move using velocity (works with Dynamic bodies)
        rb.linearVelocity = direction * es.MoveSpeed;
        
        // Calculate rotation
        float angle = Mathf.Atan2(direction. y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        
        // Smooth rotation
        transform.rotation = Quaternion. Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}