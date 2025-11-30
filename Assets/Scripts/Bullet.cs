using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;

    [SerializeField] private GameObject cannon;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 shootDirection = cannon.transform.right;
        rb.linearVelocity = shootDirection * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
