using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsData baseStats;
    private PlayerStatsData runtimeStats;

    public event Action OnStatsChanged;

    private void Awake()
    {
        // Create a runtime-safe copy of the stats
        runtimeStats = Instantiate(baseStats);
    }

    // READ-ONLY accessors (recommended)
    public float MaxHealth => runtimeStats.maxHealth;
    public float MoveSpeed => runtimeStats.moveSpeed;
    public float CannonRotationSpeed => runtimeStats.cannonRotationSpeed;
    public float Armor => runtimeStats.armor;
    public float Friction => runtimeStats.friction;

    // MODIFIERS (controlled writes)
    public void AddMaxHealth(float value)
    {
        runtimeStats.maxHealth += value;
        Notify();
    }

    public void AddMoveSpeed(float value)
    {
        runtimeStats.moveSpeed += value;
        Notify();
    }

    public void AddArmor(float value)
    {
        runtimeStats.armor += value;
        Notify();
    }

    public void SetFriction(float value)
    {
        runtimeStats.friction = value;
        Notify();
    }

    private void Notify()
    {
        OnStatsChanged?.Invoke();
    }
}