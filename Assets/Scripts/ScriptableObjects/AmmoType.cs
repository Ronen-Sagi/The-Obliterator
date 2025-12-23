using UnityEngine;

[CreateAssetMenu(fileName = "NewAmmoType", menuName = "Equipment/Ammo Type")]
public class AmmoType : ScriptableObject
{
    public string ammoName;
    public float damageMultiplier = 1f;
    public float fireRateMultiplier = 1f; // Higher is faster (or lower delay)
    public float accuracy = 1f; // 1 = perfect, lower = more spread
    public int projectileCount = 1;
    public bool piercing = false;
    public int pierceCount = 0; // How many enemies it can pierce through. 0 means none (destroys on first hit).
    public float speedMultiplier = 1f;
}
