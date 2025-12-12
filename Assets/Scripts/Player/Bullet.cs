using UnityEngine;

/// Represents a projectile that moves in a specified direction at a given speed.
public class Bullet : MonoBehaviour
{
    /// Movement speed in units per second.
    [SerializeField] public float speed = 20f;

    /// Damage by the bullet upon impact.
    [SerializeField] protected float bulletDamage;

    /// Bullet fly time
    [SerializeField] protected float flyTime = 3f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// Normalized movement direction for the bullet.
    private Vector2 direction;

    /// Initializes the bullet's movement direction.
    /// <param name="dir">The direction vector to set for bullet movement.</param>
    public void Initialize(Vector2 dir)
    {
        rb.linearVelocity = dir * speed;
        Destroy(gameObject, flyTime);
    }
}