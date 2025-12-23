using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    // Equipment
    [SerializeField] private ArmorType currentArmor;

    // Stats modified by armor
    private float armorReflectionChance = 0f;

    // Shield Bubble PowerUp
    private bool hasShieldBubble = false;

    /// Called when the player's health reaches zero (via <see cref="Health.TakeDamage"/>).
    /// Logs a diagnostic message and loads the <c>GameOverScene</c>.
    protected override void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene");
    }

    protected override void Awake()
    {
        base.Awake();
        ApplyArmorStats();
    }

    public void EquipArmor(ArmorType armor)
    {
        currentArmor = armor;
        ApplyArmorStats();
    }

    private void ApplyArmorStats()
    {
        if (currentArmor != null)
        {
            float newMaxHealth = 100f * currentArmor.healthMultiplier;
            float healthRatio = currentHealth / maxHealth;
            maxHealth = newMaxHealth;
            currentHealth = maxHealth * healthRatio;

            armorReflectionChance = currentArmor.reflectionChance;
        }
    }

    public void ActivateShield()
    {
        hasShieldBubble = true;
    }

    // Override TakeDamage to handle shield
    public new void TakeDamage(float damage)
    {
        if (hasShieldBubble)
        {
            hasShieldBubble = false;
            // Shield breaks, no damage taken
            Debug.Log("Shield Bubble blocked damage!");
            return;
        }

        base.TakeDamage(damage);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (armorReflectionChance > 0f && Random.value < armorReflectionChance)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = -rb.linearVelocity;
                    collision.gameObject.tag = "Bullet";
                }
            }
        }
    }
}
