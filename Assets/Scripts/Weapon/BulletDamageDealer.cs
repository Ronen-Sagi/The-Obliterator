using System;
using UnityEngine;

public class BulletDamageDealer : MonoBehaviour
{
    WeaponStats ws;
    [SerializeField] String tag = null;

    void Awake()
    {
        ws = GetComponent<WeaponStats>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(ws.Damage);
            Destroy(gameObject);
        }
    }
    
}