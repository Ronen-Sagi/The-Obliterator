using UnityEngine;
using UnityEngine.InputSystem;

/// Handles shooting logic for a cannon by spawning a bullet prefab at a fire point
/// and aiming toward the current mouse position in world space.
public class CannonShoot : MonoBehaviour
{
    /// Prefab of the bullet to instantiate when shooting.
    public GameObject bulletPrefab;

    /// Transform representing the spawn location and rotation for bullets.
    public Transform firePoint;

    // Equipment
    [SerializeField] private AmmoType currentAmmo;

    // Internal cooldown
    private float nextFireTime = 0f;
    private float baseFireRate = 0.5f; // Seconds between shots

    /// Checks for a left mouse button press and triggers a shot.
    void Update()
    {
        // Simple fire rate check
        if (Time.time >= nextFireTime && Mouse.current.leftButton.isPressed)
        {
            Shoot();

            float fireRateMult = currentAmmo ? currentAmmo.fireRateMultiplier : 1f;
            float effectiveInterval = baseFireRate / (fireRateMult > 0 ? fireRateMult : 0.1f);
            nextFireTime = Time.time + effectiveInterval;
        }
    }

    /// Spawns a bullet at the <see cref="firePoint"/> and initializes its movement direction
    /// based on the mouse cursor position converted to world space.
    void Shoot()
    {
        // Check for Double Tap PowerUp
        int totalVolleys = (PowerUpManager.Instance != null && PowerUpManager.Instance.IsDoubleTapActive) ? 2 : 1;

        for (int v = 0; v < totalVolleys; v++)
        {
            float damageMult = currentAmmo ? currentAmmo.damageMultiplier : 1f;
            float speedMult = currentAmmo ? currentAmmo.speedMultiplier : 1f;
            float accuracy = currentAmmo ? currentAmmo.accuracy : 1f;
            bool piercing = currentAmmo ? currentAmmo.piercing : false;
            int pierceCount = currentAmmo ? currentAmmo.pierceCount : 0;
            int baseProjectileCount = currentAmmo ? currentAmmo.projectileCount : 1;

            // "AP Rounds (Armor Piercing): Pierces through one enemy target."
            // This implies it hits 2 targets total (1st and 2nd). So pierceCount should be 1.
            // If piercing is true but pierceCount is 0, we should probably set it to at least 1?
            // Or rely on config.
            if (piercing && pierceCount == 0) pierceCount = 1;

            for (int i = 0; i < baseProjectileCount; i++)
            {
                // Calculate spread
                float spreadAngle = 0f;
                if (baseProjectileCount > 1 || accuracy < 1f)
                {
                    float maxSpread = 30f * (1f - accuracy);
                    if (baseProjectileCount > 1) maxSpread = 30f;

                    spreadAngle = Random.Range(-maxSpread, maxSpread);
                }

                if (v > 0) spreadAngle += Random.Range(-5f, 5f);

                Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, spreadAngle);

                GameObject bulletObj = Instantiate(
                    bulletPrefab,
                    firePoint.position,
                    rotation
                );

                Vector2 dir = rotation * Vector3.right;

                Bullet bulletScript = bulletObj.GetComponent<Bullet>();
                bulletScript.speed *= speedMult;
                bulletScript.Initialize(dir);

                // Configure piercing on bullet
                if (piercing)
                {
                    bulletScript.enablePiercing = true;
                    bulletScript.SetMaxPierce(pierceCount);
                }

                DamageDealer damageDealer = bulletObj.GetComponent<DamageDealer>();
                if (damageDealer != null)
                {
                    damageDealer.SetDamage(10f * damageMult);
                }
            }
        }
    }

    public void EquipAmmo(AmmoType ammo)
    {
        currentAmmo = ammo;
    }
}
