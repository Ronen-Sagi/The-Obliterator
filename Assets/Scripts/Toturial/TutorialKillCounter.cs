using UnityEngine;

public class TutorialKillCounter : MonoBehaviour
{
    private TutorialManager tutorialManager;
    private int killCounter = 0;
    private bool isActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    public void ActivateCounter()
    {
        isActive = true;
        killCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
    }

    public void OnEnemyKilled()
    {
        if (!isActive)
        {
            return;
        }

        killCounter++;
        if (tutorialManager != null)
        {
            tutorialManager.UpdateKillCount(killCounter);
        }
    }
}