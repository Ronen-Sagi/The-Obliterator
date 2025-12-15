using UnityEngine;

public class XPDrop : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float startForce = 5f;
    [SerializeField] float friction = 6f;

    Vector2 velocity;

    void Start()
    {
        // Random direction
        Vector2 direction = Random.insideUnitCircle.normalized;
        velocity = direction * startForce;
    }

    void Update()
    {
        // Move
        transform.position += (Vector3)(velocity * Time.deltaTime);

        // Apply friction (smooth slow down)
        velocity = Vector2.Lerp(velocity, Vector2.zero, friction * Time.deltaTime);
    }
}