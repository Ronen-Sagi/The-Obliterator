using UnityEngine;

/// Defines the type of an enemy.
public enum EnemyType
{
    Swarmer,
    Tanker,
    Ranger
}

/// Base class for configuring enemy behavior and stats.
[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Enemy/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    public EnemyType type;
    public float baseHealth = 10f;
    public float baseDamage = 10f;
    public float baseSpeed = 2f;
    public float knockbackResistance = 0f; // 0 to 1
}
