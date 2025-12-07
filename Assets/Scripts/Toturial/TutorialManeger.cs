using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionText;
    
    [SerializeField] private GameObject dot1;
    [SerializeField] private GameObject dot2;
    
    [SerializeField] private GameObject tutorialEnemy;
    
    [SerializeField] private GameObject player;
    
    private int currentStep = 0;

    void Start()
    {
        instructionText.text = "Welcome!  Use WASD keys to move your character.\nTouch the dot to continue. ";
        
        dot1. SetActive(true);
        dot2.SetActive(false);
        
        if (tutorialEnemy != null)
        {
            tutorialEnemy.SetActive(false);
        }
    }
    
    public void OnDotTouched(int dotNumber)
    {
        if (dotNumber == 1 && currentStep == 0)
        {
            currentStep = 1;
            dot2.SetActive(true);
            instructionText.text = "Great! Now move to the second dot.";
        }
        else if (dotNumber == 2 && currentStep == 1)
        {
            // Second dot touched - start shooting tutorial
            currentStep = 2;
            instructionText.text = "Now let's learn to shoot!\nAim with the mouse and click the left button to shoot at the enemy.";
            
            if (tutorialEnemy != null)
            {
                tutorialEnemy.SetActive(true);
            }
        }
    }
    
    public void OnEnemyDestroyed()
    {
        if (currentStep == 2)
        {
            currentStep = 3;
            instructionText.text = "Great job! Tutorial complete.\nLoading game...";
            //LoadMainGame();
        }
    }

    private void LoadMainGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}