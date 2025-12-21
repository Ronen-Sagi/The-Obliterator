using UnityEngine;

/// Represents a projectile that moves in a specified direction at a given speed.
public class Bullet : MonoBehaviour
{
    /// Movement speed in units per second.
    [SerializeField] public float speed = 20f;

    /// Bullet fly time
    [SerializeField] protected float flyTime = 3f;

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

    /// Called when the bullet collides with another object.
    /// Destroys the bullet if it hits a wall.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet hit a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
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