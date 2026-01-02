using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Identity")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Combat")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float maxHealth = 50f;

    public event Action OnStatsChanged;


    public GameObject EnemyPrefab => enemyPrefab;

    public float Damage
    {
        get => damage;
        set
        {
            damage = value;
            Notify();
        }
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
            Notify();
        }
    }

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            Notify();
        }
    }

    // ---- Helper ----
    private void Notify()
    {
        OnStatsChanged?.Invoke();
    }
}