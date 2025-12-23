using UnityEngine;

/// Represents a projectile that moves in a specified direction at a given speed.
public class Bullet : MonoBehaviour
{
    /// Movement speed in units per second.
    [SerializeField] public float speed = 20f;

    /// Bullet fly time
    [SerializeField] protected float flyTime = 3f;

    public bool enablePiercing = false;
    private int pierceCount = 0;
    private int maxPierce = 1;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// Initializes the bullet's movement direction.
    /// <param name="dir">The direction vector to set for bullet movement.</param>
    public void Initialize(Vector2 dir)
    {
        rb.linearVelocity = dir * speed;
        Destroy(gameObject, flyTime);
    }

    public void SetMaxPierce(int max)
    {
        maxPierce = max;
    }

    /// Called when the bullet collides with another object.
    /// Destroys the bullet if it hits a wall.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet hit a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Apply knockback
            EnemyMovement enemyMove = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemyMove != null)
            {
                // Knockback direction is bullet direction
                Vector2 knockDir = rb.linearVelocity.normalized;
                // Force amount? Let's say 5f for now or based on damage/speed.
                float force = 5f;
                enemyMove.ApplyKnockback(knockDir * force);
            }

            if (enablePiercing)
            {
                pierceCount++;
                if (pierceCount >= maxPierce)
                {
                    Destroy(gameObject);
                }
            }
            // If not piercing, Destroy is handled by DamageDealer usually?
            // DamageDealer checks collision.gameObject matches tag.
            // If Bullet has DamageDealer, it destroys itself if it hits target.
            // But here we might destroy it too?
            // If DamageDealer destroys it, this OnCollisionEnter2D runs before or after?
            // They are on the same object. Unity order is undefined but usually both run.
            // If DamageDealer destroys it, code here continues until end of frame.
        }
    }

    /// Alternative:  Called when the bullet enters a trigger collider.
    /// Use this if your walls use trigger colliders instead of solid colliders.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
