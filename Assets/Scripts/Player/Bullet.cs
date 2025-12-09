using UnityEngine;

/// Represents a projectile that moves in a specified direction at a given speed.
public class Bullet : MonoBehaviour
{
    /// Movement speed in units per second.
    [SerializeField] public float speed = 20f;

    /// Damage by the bullet upon impact.
    [SerializeField] protected float bulletDamage;

    /// Normalized movement direction for the bullet.
    private Vector2 direction;

    /// Initializes the bullet's movement direction.
    /// <param name="dir">The direction vector to set for bullet movement.</param>
    public void Initialize(Vector2 dir)
    {
        direction = dir;
    }

    /// Updates the bullet's position every frame based on direction and speed.
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}