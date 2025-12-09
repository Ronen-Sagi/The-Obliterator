using UnityEngine;

/// Tracks tutorial enemy kills and reports updates to the <see cref="TutorialManager"/>.
public class TutorialKillCounter : MonoBehaviour
{
    /// Reference to the tutorial manager that receives kill count updates.
    private TutorialManager tutorialManager;

    /// Current number of enemies killed since activation.
    private int killCounter = 0;

    /// Indicates whether the counter is currently active and should record kills.
    private bool isActive = false;

    /// Locates and caches a reference to <see cref="TutorialManager"/> in the scene.
    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    /// Activates the kill counter and resets the count to zero.
    public void ActivateCounter()
    {
        isActive = true;
        killCounter = 0;
    }

    /// Early exits when the counter is not active.
    void Update()
    {
        if (!isActive)
        {
            return;
        }
    }

    /// Increments the kill counter if active and notifies the tutorial manager of the new count.
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