using System;
using UnityEngine;

/// Deals damage to other 2D collision targets.
public class DamageDealer : MonoBehaviour
{
    /// Amount of damage applied to the target's <see cref="Health"/> on a valid collision.
    [SerializeField] float damage = 10;

    /// Tag that the collided GameObject must have in order to receive damage.
    [SerializeField] String tag = null;

    /// Unity 2D physics callback invoked when this collider begins touching another collider.
    /// <param name="collision">Collision data for the contact.</param>
    /// \- Only applies damage when <c>collision.gameObject</c> matches <see cref="tag"/>.\n
    /// \- Uses a null\-conditional call so missing <see cref="Health"/> does not throw.\n
    /// \- Destroys this GameObject if it is tagged <c>Bullet</c> after dealing damage.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);

            if (gameObject.CompareTag("Bullet"))
            {
                // Check if the bullet should persist (e.g. piercing)
                Bullet bullet = GetComponent<Bullet>();
                if (bullet != null && bullet.enablePiercing)
                {
                    // Bullet handles its own destruction logic
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
}
