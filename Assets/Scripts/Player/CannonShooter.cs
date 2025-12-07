using UnityEngine;
using UnityEngine.InputSystem;

public class CannonShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Spawn bullet
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get mouse world position
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z)
        );

        // Calculate direction
        Vector2 dir = (mouseWorld - firePoint.position).normalized;

        // Initialize bullet with only direction
        bulletObj.GetComponent<Bullet>().Initialize(dir);
    }
}