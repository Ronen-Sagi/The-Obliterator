using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private void OnDestroy()
    {
        TutorialKillCounter killCounter = FindObjectOfType<TutorialKillCounter>();
        if (killCounter != null)
        {
            killCounter.OnEnemyKilled();
        }
    }
}