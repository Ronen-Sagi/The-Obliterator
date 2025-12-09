using UnityEngine;

/// Tutorial enemy that listens for collisions with bullets.
/// When hit, notifies the <see cref="TutorialManager"/> and destroys both the bullet and itself.
public class TutorialEnemy : MonoBehaviour
{
    /// Reference to the tutorial manager coordinating tutorial progression.
    private TutorialManager tutorialManager;

    /// Caches a reference to the active <see cref="TutorialManager"/> in the scene.
    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    /// Trigger callback invoked when another collider enters this enemy's trigger.
    /// If the collider is tagged as "Bullet", notifies the tutorial manager and destroys both objects.
    /// <param name="other">The collider that entered this trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (tutorialManager != null)
            {
                tutorialManager.OnEnemyDestroyed();
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}