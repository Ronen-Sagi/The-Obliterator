using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene");
        
    }
}
