using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/Enemy")]
public class EnemyConfig : ScriptableObject
{
    public String enemyName;
    public float maxHealth;
    public float moveSpeed;
    public float damage;
    
}
