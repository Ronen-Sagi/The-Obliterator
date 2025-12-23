using System.Collections;
using UnityEngine;

/// <summary>
/// Manages active power-ups on the player.
/// </summary>
public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    // State flags
    public bool IsDoubleTapActive { get; private set; }
    public bool IsXPSurgeActive { get; private set; }
    public float XPSurgeMultiplier { get; private set; } = 1f;

    // References
    private CannonShoot cannonShoot;
    private CircleCollider2D pickupCollider;
    private PlayerHealth playerHealth;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        cannonShoot = GetComponent<CannonShoot>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
         // Assume we are on the Player object
         // Find a trigger collider that might be used for pickup
         Collider2D[] cols = GetComponents<Collider2D>();
         foreach(var col in cols)
         {
             if (col.isTrigger && col is CircleCollider2D circle)
             {
                 pickupCollider = circle;
                 break;
             }
         }

         // If no dedicated pickup collider found, we might add one or use the main one if it's a trigger
         // But main collider is usually not trigger for physics.
         // If none found, we can't implement magnet easily without adding component.
         if (pickupCollider == null)
         {
             // Create one for pickup purposes?
             // Or maybe CoinDrop checks collision with non-trigger too?
             // PickupDrop: OnTriggerEnter2D.
             // So player needs a Trigger collider.
             // Let's ensure there is one.
         }
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        StartCoroutine(HandlePowerUp(powerUp));
    }

    private IEnumerator HandlePowerUp(PowerUp powerUp)
    {
        switch (powerUp.type)
        {
            case PowerUpType.CoinMagnet:
                float originalRadius = 0.5f;
                if (pickupCollider != null)
                {
                    originalRadius = pickupCollider.radius;
                    pickupCollider.radius *= 3f;
                }
                else
                {
                    // Try to add one temporarily?
                    // Safe to just log warning if not setup
                    Debug.LogWarning("No pickup collider found for Coin Magnet");
                }

                yield return new WaitForSeconds(powerUp.duration);

                if (pickupCollider != null) pickupCollider.radius = originalRadius;
                break;

            case PowerUpType.TimeFreeze:
                EnemyMovement[] enemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
                foreach (var enemy in enemies) enemy.enabled = false;

                yield return new WaitForSeconds(powerUp.duration);

                 enemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
                foreach (var enemy in enemies) enemy.enabled = true;
                break;

            case PowerUpType.DoubleTap:
                IsDoubleTapActive = true;
                yield return new WaitForSeconds(powerUp.duration);
                IsDoubleTapActive = false;
                break;

            case PowerUpType.ShieldBubble:
                if (playerHealth != null)
                {
                    playerHealth.ActivateShield();
                }
                break;

            case PowerUpType.XPSurge:
                IsXPSurgeActive = true;
                XPSurgeMultiplier = powerUp.multiplier;
                yield return new WaitForSeconds(powerUp.duration);
                IsXPSurgeActive = false;
                XPSurgeMultiplier = 1f;
                break;
        }
    }
}
