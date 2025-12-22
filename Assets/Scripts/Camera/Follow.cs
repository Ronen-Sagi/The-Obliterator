using UnityEngine;


///Moves camera position toward the target.
public class Follow : MonoBehaviour
{
    /// The transform the camera will follow. 
    [SerializeField] Transform target;


    /// The smoothing time (in seconds).
    [SerializeField] float smoothSpeed = 0.15f;

    /// Internal velocity value required by <see cref="Vector3.SmoothDamp"/>.
    /// This is maintained across frames to produce smooth damping.
    Vector3 velocity = Vector3.zero;

    /// Moves the camera smoothly toward the target's x/y position while keeping the camera's z position unchanged.
    void LateUpdate()
    {
        // If there is no target assigned, exit early.
        if (!target) return;

        // Desired position matches target's x/y but preserves current z (camera depth).
        Vector3 desiredPosition = target.position;
        desiredPosition.z = transform.position.z;

        // Smoothly move the camera toward the desired position using damping.
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothSpeed
        );
    }
}