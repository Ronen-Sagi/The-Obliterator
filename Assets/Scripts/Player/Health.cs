using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    private float currentHealth;
    
    [SerializeField] private float deathDelay = 2;

    public float GetMaxHealth() { return maxHealth; }
    public float GetCurrentHealth() { return currentHealth; }
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Player died");
        // Disable movement / play animation / restart level
        Invoke(nameof(GameOver), deathDelay);
        Destroy(gameObject);
    }
    
    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}