using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene");
        
    }
}
