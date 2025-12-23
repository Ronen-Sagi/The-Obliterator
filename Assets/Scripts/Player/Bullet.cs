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
            if (enablePiercing)
            {
                pierceCount++;
                // maxPierce is total targets to pierce.
                // If maxPierce is 1, it hits 1st, count becomes 1. 1 >= 1? Destroy.
                // This means it hits 1 enemy.
                // Requirement "AP Rounds... Pierces through one enemy target".
                // Means it hits Enemy A (passes through) and hits Enemy B (destroys?).
                // So it should hit 2 targets?
                // "Pierces through one" means it goes through one.
                // So maxPierce should be 2 (hit 1, keep going, hit 2, stop?). Or infinite but count is 1?
                // Usually "Pierce 1" means Hit + 1 more.
                // If maxPierce is "Number of EXTRA enemies to hit", then loop should be `if (pierceCount > maxPierce)`.
                // If maxPierce is "Total enemies to hit", then `if (pierceCount >= maxPierce)`.

                // Let's assume maxPierce is Total Hits.
                // If I want to pierce 1 enemy, I want to hit 2 enemies total.
                // So maxPierce should be 2.
                // CannonShoot sets it based on config.

                if (pierceCount >= maxPierce)
                {
                    Destroy(gameObject);
                }
            }
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
