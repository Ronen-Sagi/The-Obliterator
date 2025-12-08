using UnityEngine;

public class TutorialRealEnemy : EnemySpawner
{
    public TutorialManager tutorialManager;

    void OnDestroy()
    {
        if (tutorialManager != null)
        {
            tutorialManager. OnRealEnemyDestroyed();
        }
    }
}
