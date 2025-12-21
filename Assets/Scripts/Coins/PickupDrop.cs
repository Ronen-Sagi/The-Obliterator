using UnityEngine;

public abstract class PickupDrop : MonoBehaviour
{
    [SerializeField] protected float startForce = 5f;
    [SerializeField] protected float friction = 6f;

    protected Vector2 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        velocity = direction * startForce;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(velocity * Time.deltaTime);
        velocity = Vector2.Lerp(velocity, Vector2.zero, friction * Time.deltaTime);
    }

    protected abstract void OnCollected(GameObject player);

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected(other.gameObject);
            Destroy(gameObject);
        }
    }
}