using UnityEngine;
using Random = UnityEngine.Random;

public abstract class PickupDrop : MonoBehaviour
{
    [SerializeField] protected float startForce = 5f;
    [SerializeField] protected float friction = 6f;

    protected Vector2 velocity;
    private Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        velocity = direction * startForce;
    }

    private void FixedUpdate()
    {
        if (rb != null)
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        else
            transform.position += (Vector3)(velocity * Time.fixedDeltaTime);

        velocity = Vector2.Lerp(velocity, Vector2.zero, friction * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        OnCollected(other.gameObject);
        Destroy(gameObject);
    }

    protected abstract void OnCollected(GameObject player);
}