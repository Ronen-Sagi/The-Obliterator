using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;  

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
