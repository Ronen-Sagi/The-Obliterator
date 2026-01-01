using UnityEngine;

public class BulletCollisions : MonoBehaviour
{
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
}