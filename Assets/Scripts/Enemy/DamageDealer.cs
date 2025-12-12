using System;
using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // Damage
    [SerializeField] float damage = 10;
    
    // the tag of who to deal damage to
    [SerializeField] String tag = null;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);
            
            if (gameObject.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        }
    }
}