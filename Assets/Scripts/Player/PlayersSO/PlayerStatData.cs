using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Stats Data")]
public class PlayerStatsData : ScriptableObject
{
    public float maxHealth;
    public float moveSpeed;
    public float cannonRotationSpeed;
    public float armor;
    public float friction;
}