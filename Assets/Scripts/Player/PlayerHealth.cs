using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    /// Called when the player's health reaches zero (via <see cref="Health.TakeDamage"/>).
    /// Logs a diagnostic message and loads the <c>GameOverScene</c>.
    protected override void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene");
    }
}