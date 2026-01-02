using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats PS;
    
    [Header("Base Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float cannonRotationSpeed = 180f;
    [SerializeField] private float armor = 0f;
    [SerializeField] private float friction = 5f;

    public event Action OnStatsChanged;
    
    private void Awake()
    {
        if (PS != null && PS != this)
        {
            Destroy(gameObject);
            return;
        }

        PS = this;
        DontDestroyOnLoad(gameObject);
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

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
            Notify();
        }
    }

    public float CannonRotationSpeed
    {
        get => cannonRotationSpeed;
        set
        {
            cannonRotationSpeed = value;
            Notify();
        }
    }

    public float Armor
    {
        get => armor;
        set
        {
            armor = value;
            Notify();
        }
    }

    public float Friction
    {
        get => friction;
        set
        {
            friction = value;
            Notify();
        }
    }

    private void Notify()
    {
        OnStatsChanged?.Invoke();
    }
}