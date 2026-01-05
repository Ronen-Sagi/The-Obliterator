using System;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    
    [Header("Visuals")]
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Stats")]
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float flyTime;
    [SerializeField] private int bulletCount;
    [SerializeField] private float bulletSpeed;
    
    [Header("Audio")]
    [SerializeField] private AudioClip shootSound;
    
    private void Awake()
    {
        LoadWeaponData(weaponData);
    }
    
    private void LoadWeaponData(WeaponData data)
    {
        if (data == null) return;
        
        bulletPrefab = data.bulletPrefab;
        fireRate = data.fireRate;
        damage = data.damage;
        flyTime = data.flyTime;
        bulletCount = data.bulletCount;
        bulletSpeed = data.bulletSpeed;
        shootSound = data.shootSound;
    }
    
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
    
    public AudioClip ShootSound
    {
        get => shootSound;
        set
        {
            shootSound = value;
            Notify();
        }
    }
    
    private void Notify()
    {
        OnStatChanged?.Invoke();
    }
}
