using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    PlayerStats ps;
    
    /// Called when the player's health reaches zero (via <see cref="Health.TakeDamage"/>).
    /// Logs a diagnostic message and loads the <c>GameOverScene</c>.
    protected override void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene");
    }
    
    protected override void Awake()
    {
        ps = GetComponent<PlayerStats>();
        SampleStats();
        base.Awake();
    }

    private void SampleStats()
    {
        maxHealth = ps.MaxHealth;
    }

    private void OnEnable()
    {
        ps.OnStatsChanged += SampleStats;
    }

    private void OnDisable()
    {
        ps.OnStatsChanged -= SampleStats;
    }
}