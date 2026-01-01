using System;
using Unity.VisualScripting;
using UnityEngine;

/// Deals damage to other 2D collision targets.
public class DamageDealer : MonoBehaviour
{
    private EnemyStats es;

    /// Tag that the collided GameObject must have in order to receive damage.
    [SerializeField] String tag = null;

    private void Awake()
    {
        es = GetComponent<EnemyStats>();
    }

    /// Unity 2D physics callback invoked when this collider begins touching another collider.
    /// <param name="collision">Collision data for the contact.</param>
    /// \- Only applies damage when <c>collision.gameObject</c> matches <see cref="tag"/>.\n
    /// \- Uses a null\-conditional call so missing <see cref="Health"/> does not throw.\n
    /// \- Destroys this GameObject if it is tagged <c>Bullet</c> after dealing damage.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(es.damage);
        }
    }
}