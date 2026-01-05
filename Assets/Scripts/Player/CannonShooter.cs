using UnityEngine;
using UnityEngine.InputSystem;

/// Handles shooting logic for a cannon by spawning a bullet prefab at a fire point
/// and aiming toward the current mouse position in world space.
public class CannonShoot : MonoBehaviour
{
    WeaponManager wp = WeaponManager.Instance;
    
    /// Prefab of the bullet to instantiate when shooting.
    private GameObject weaponPrefab;

    /// Transform representing the spawn location and rotation for bullets.
    public Transform firePoint;
    void Awake()
    {
        EquipWeapon(wp.CurrentWeapon);
    }

    /// Checks for a left mouse button press and triggers a shot.
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    /// Spawns a bullet at the <see cref="firePoint"/> and initializes its movement direction
    /// based on the mouse cursor position converted to world space.
    void Shoot()
    {
        GameObject bulletObj = Instantiate(
            weaponPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Vector2 dir = firePoint.right; // direction the cannon is facing

        bulletObj.GetComponent<WeaponShooter>().Initialize(dir);
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