using UnityEngine;
using UnityEngine.InputSystem;

/// Rotates the cannon to smoothly aim toward the current mouse position in world space.
[RequireComponent(typeof(PlayerStats))]
public class canonMovement : MonoBehaviour
{
    PlayerStats ps;
    
    /// Speed factor controlling how quickly the cannon rotates toward the target angle.
    [SerializeField] private float rotationSpeed = 5f;

    void Awake()
    {
        ps = GetComponent<PlayerStats>();
        
        if (ps == null)
        {
            Debug.LogError("canonMovement: Missing PlayerStats on this object", this);
            enabled = false;
            return;
        }
        
        rotationSpeed = ps.CannonRotationSpeed;
    }
    /// Reads the mouse position, converts it to world space, computes the target angle,
    /// and applies a smoothed rotation to the cannon.
    void Update()
    {
        // Read the current mouse position from the Input System in screen coordinates.
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        // Convert the screen position to world space using the main camera.
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, -Camera.main.transform.position.z)
        );

        // Compute the direction from the cannon to the mouse position.
        Vector2 dir = mouseWorldPos - transform.position;

        // Compute the target rotation angle in degrees using the direction vector.
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Smoothly interpolate the current angle toward the target angle using rotationSpeed.
        float angle = Mathf.MoveTowardsAngle(
            transform.eulerAngles.z,
            targetAngle,
            rotationSpeed * Time.deltaTime // degrees per second
        );

        // Apply the interpolated rotation around the Z axis.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}