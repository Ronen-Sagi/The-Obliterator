using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject xp;
    protected override void Die()
    {
        Instantiate(xp, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
