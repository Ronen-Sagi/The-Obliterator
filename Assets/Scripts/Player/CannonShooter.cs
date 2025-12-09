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
        // Spawn bullet
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get mouse world position
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));

        // Calculate direction
        Vector2 dir = (mouseWorld - firePoint.position).normalized;

        // Initialize bullet with only direction
        bulletObj.GetComponent<Bullet>().Initialize(dir);
    }
}