using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    
    private TutorialManager tutorialManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
    
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
