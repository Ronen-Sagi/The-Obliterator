using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 20f;
    [SerializeField] protected float bulletDamage;

    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}