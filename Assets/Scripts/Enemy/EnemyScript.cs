using UnityEngine;

/// Handles enemy collision logic in a 2D environment.
/// When the enemy collides with a `Bullet`, it increments the tutorial kill counter (if present)
/// and destroys both the enemy and the bullet.
public class EnemyScript : MonoBehaviour
{
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is tagged as a bullet.
        if (other.CompareTag("Bullet"))
        {
            // Attempt to find the tutorial kill counter in the scene and update it.
            TutorialKillCounter killCounter = FindObjectOfType<TutorialKillCounter>();
            if (killCounter != null)
            {
                killCounter.OnEnemyKilled();
            }

            // Destroy this enemy and the bullet upon collision.
            //Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    
    

}