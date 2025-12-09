using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other. CompareTag("Bullet"))
        {
            TutorialKillCounter killCounter = FindObjectOfType<TutorialKillCounter>();
            if (killCounter != null)
            {
                killCounter.OnEnemyKilled();
            }
        
            Destroy(gameObject);
            Destroy(other. gameObject);
        }
    }
}