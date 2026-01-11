using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(
        Vector2 direction,
        float speed,
        float damage,
        float lifetime,
        AudioClip shootSound = null
    )
    {
        this.damage = damage;
        rb.linearVelocity = direction.normalized * speed;

        if (shootSound != null && TryGetComponent(out AudioSource audio))
        {
            audio.PlayOneShot(shootSound);
        }

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
        
        // Check if the bullet hit a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}