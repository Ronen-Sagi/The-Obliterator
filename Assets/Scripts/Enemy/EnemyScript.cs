using System;
using UnityEngine;

/// Notifies the tutorial kill counter when this enemy GameObject is destroyed.
public class EnemyScript : MonoBehaviour
{
    private void OnDestroy()
    {
        // Look up the tutorial kill counter in the current scene.
        TutorialKillCounter killCounter = FindObjectOfType<TutorialKillCounter>();

        // If present, increment/notify the tutorial counter that an enemy was killed.
        if (killCounter != null)
        {
            killCounter.OnEnemyKilled();
        }
    }
}