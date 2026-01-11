using System;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private WeaponData baseData;
    private WeaponData runtimeData;

    public event Action OnStatChanged;

    public void Initialize()
    {
        // Runtime-safe clone
        runtimeData = Instantiate(baseData);
        Notify();
        Debug.Log(runtimeData.name + " initialized.");
    }

    public GameObject ProjectilePrefab => runtimeData.bulletPrefab;
    public float FireRate => runtimeData.fireRate;
    public float Damage => runtimeData.damage;
    public float FlyTime => runtimeData.flyTime;
    public int BulletCount => runtimeData.bulletCount;
    public float BulletSpeed => runtimeData.bulletSpeed;
    public AudioClip ShootSound => runtimeData.shootSound;

    // MODIFIERS
    public void AddDamage(float value)
    {
        runtimeData.damage += value;
        Notify();
    }

    public void AddFireRate(float value)
    {
        runtimeData.fireRate += value;
        Notify();
    }

    public void AddBulletCount(int value)
    {
        runtimeData.bulletCount += value;
        Notify();
    }

    private void Notify()
    {
        OnStatChanged?.Invoke();
    }
}