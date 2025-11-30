using UnityEngine;
using UnityEngine.InputSystem;

public class canonMovement : MonoBehaviour
{
    public float rotationSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, - Camera.main.transform.position.z)
        );

        Vector2 dir = mouseWorldPos - transform.position;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Smooth rotation
        float angle = Mathf.LerpAngle(
            transform.eulerAngles.z,
            targetAngle,
            rotationSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
