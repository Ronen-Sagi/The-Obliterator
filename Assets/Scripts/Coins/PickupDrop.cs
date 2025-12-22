using UnityEngine;

/// Base class for pickup items that spawn with an initial impulse and quickly slow down,
/// then get collected when the Player enters the trigger.
/// NOT YET IMPLEMENTED PROPERLY.
public abstract class PickupDrop : MonoBehaviour
{
    /// Initial movement speed applied in a random 2D direction on <see cref="Start"/>.
    [SerializeField] protected float startForce = 5f;

    /// Damping factor used to lerp velocity toward zero each frame.
    /// Higher values stop the pickup faster.
    [SerializeField] protected float friction = 6f;

    /// Current 2D velocity used for manual movement .
    protected Vector2 velocity;

    /// Initializes the pickup with a random direction and initial velocity.
    /// Uses <see cref="Random.insideUnitCircle"/> and normalizes it to get a unit direction.
    protected virtual void Start()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        velocity = direction * startForce;
    }

    /// Moves the pickup each frame and damps its velocity toward zero.
    void Update()
    {
        transform.position += (Vector3)(velocity * Time.deltaTime);
        velocity = Vector2.Lerp(velocity, Vector2.zero, friction * Time.deltaTime);
    }

    /// Called when the pickup is collected by the Player.
    /// <param name="player">The player GameObject that collected this pickup.</param>
    /// Implementations should apply the pickup effect. The pickup is destroyed immediately after this call.
    protected abstract void OnCollected(GameObject player);

    /// Trigger callback used to detect collection by the Player.
    /// <param name="other">The collider entering this pickup's trigger.</param>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected(other.gameObject);
            Destroy(gameObject);
        }
    }
}