using UnityEngine;

/// Represents a projectile that moves in a specified direction at a given speed.
public class WeaponShooter : MonoBehaviour
{
    
    Rigidbody2D rb;
    WeaponStats ws;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ws = GetComponent<WeaponStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// Initializes the bullet's movement direction.
    /// <param name="dir">The direction vector to set for bullet movement.</param>
    public void Initialize(Vector2 dir)
    {
        rb.linearVelocity = dir * ws.BulletSpeed;
        audioSource.PlayOneShot(ws.ShootSound);
        Destroy(gameObject, ws.FlyTime);
        
    }

}