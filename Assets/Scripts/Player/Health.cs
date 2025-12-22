using UnityEngine;

/// Tracks and manages the health.
public class Health : MonoBehaviour
{
    /// The maximum health value the player can have.
    [SerializeField] float maxHealth = 100;

    /// The player's current health.
    private float currentHealth;

    /// Gets the configured maximum health.
    /// <returns>The maximum health value.</returns>
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /// Gets the current health.
    /// <returns>The current health value.</returns>
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    /// Initializes current health to the maximum.
    void Awake()
    {
        currentHealth = maxHealth;
    }

    /// Applies damage to the player by reducing <see cref="currentHealth"/>.
    /// If health drops to zero or below, <see cref="Die"/> is called.
    /// <param name="damage">Amount of damage to apply.</param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// Handles death behavior.
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}