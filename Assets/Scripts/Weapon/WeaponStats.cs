using System;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float flyTime;
    [SerializeField] private int bulletCount;
    [SerializeField] private float bulletSpeed;
    
    public event Action OnStatChanged;

    public float FireRate
    {
        get => fireRate;
        set
        {
            fireRate = value;
            Notify();
        }
    }
    public float Damage
    {
        get => damage;
        set
        {
            damage = value;
            Notify();
        }
    }

    public float FlyTime
    {
        get => flyTime;
        set
        {
            flyTime = value;
            Notify();
        }
    }
    public int BulletCount
    {
        get => bulletCount;
        set
        {
            bulletCount = value;
            Notify();
        }
    }
    public float BulletSpeed
    {
        get => bulletSpeed;
        set
        {
            bulletSpeed = value;
            Notify();
        }
    }
    
    private void Notify()
    {
        OnStatChanged?.Invoke();
    }
}
