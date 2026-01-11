using UnityEngine;
using UnityEngine.InputSystem;

/// Handles shooting logic for a cannon by spawning a bullet prefab at a fire point
/// and aiming toward the current mouse position in world space.
public class CannonShoot : MonoBehaviour
{
    WeaponManager wm = WeaponManager.Instance;
    
    /// Prefab of the bullet to instantiate when shooting.
    private GameObject weaponPrefab;
    private WeaponStats ws;
    private GameObject projectilePrefab;

    /// Transform representing the spawn location and rotation for bullets.
    public Transform firePoint;
    
    private float timer = 0f;
    void Awake()
    {
        EquipWeapon(wm.CurrentWeapon);
        ws = weaponPrefab.GetComponent<WeaponStats>();
        Debug.Log("Equipped weapon: " + weaponPrefab.name);
        Debug.Log("Firerate: " + weaponPrefab.GetComponent<WeaponStats>().FireRate);
    }

    /// Checks for a left mouse button press and triggers a shot.
    void Update()
    {
        timer += Time.deltaTime;
        if (Mouse.current.leftButton.isPressed &&
            timer>= wm.CurrentWeapon.GetComponent<WeaponStats>().FireRate)
        {
            timer = 0f;
            Shoot();
        }
    }

    /// Spawns a bullet at the <see cref="firePoint"/> and initializes its movement direction
    /// based on the mouse cursor position converted to world space.
    void Shoot()
    {
        GameObject projectileObj = Instantiate(
            ws.ProjectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Vector2 dir = firePoint.right; // direction the cannon is facing

        projectileObj.GetComponent<Projectile>().Initialize(dir, 
            ws.BulletSpeed, 
            ws.Damage, 
            ws.FlyTime,
            ws.ShootSound
        );
    }
    
    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        if (newWeaponPrefab == null)
        {
            Debug.LogWarning("EquipWeapon: newWeaponPrefab is null");
            return;
        }
        weaponPrefab = newWeaponPrefab;
    }
}