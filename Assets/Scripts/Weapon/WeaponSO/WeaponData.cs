using UnityEngine;

[CreateAssetMenu(
    fileName = "NewWeapon",
    menuName = "Weapon/WeaponData"
)]
public class WeaponData : ScriptableObject
{
    [Header("Identity")]
    public WeaponName weaponName;

    [Header("Projectile")]
    public GameObject bulletPrefab;

    [Header("Base Stats")]
    public float fireRate = 1f;
    public float damage = 10f;
    public float flyTime = 2f;
    public int bulletCount = 1;
    public float bulletSpeed = 10f;

    [Header("Visuals")]
    public Sprite icon;
    
    [Header("Audio")]
    public AudioClip shootSound;
}

public enum WeaponName
{
    LaserShooter,
    Blaster,
    Sniper
}