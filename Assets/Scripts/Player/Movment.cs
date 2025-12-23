using UnityEngine;
using UnityEngine.InputSystem;

/// Handles 2D player movement using the Unity Input System with WASD bindings,
/// applying impulse forces, friction-based deceleration and a temporary speed boost.
/// More kind of boost will be added later.
public class Movment : MonoBehaviour
{
    /// Input actions for moving 
    [SerializeField] private InputAction up = new InputAction(type: InputActionType.Button);

    [SerializeField] private InputAction down = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction left = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction right = new InputAction(type: InputActionType.Button);

    /// Impulse force applied per input press to accelerate the player.
    [SerializeField] private float moveSpeed = 5f;

    /// Maximum allowed linear speed.
    [SerializeField] private float maxMoveSpeed = 5f;

    /// Friction factor used to lerp velocity toward zero each frame.
    [SerializeField] private float friction = 5f;

    /// Multiplier applied to speed and max speed during a speed boost.
    [SerializeField] private float speedBoostMultiplier = 2f;

    // Equipment
    [SerializeField] private WheelType currentWheels;
    [SerializeField] private ArmorType currentArmor;

    /// Cached reference to the <see cref="Rigidbody2D"/> controlling physics-based movement.
    private Rigidbody2D rb;

    /// Original configured move speed before any boosts.
    private float originalMoveSpeed;

    /// Original configured max move speed before any boosts.
    private float originalMaxMoveSpeed;
    private float originalFriction;

    /// Indicates whether a speed boost is currently active.
    private bool isSpeedBoosted = false;

    /// Initializes input bindings and caches original speed values.
    void Start()
    {
        up.AddBinding("<Keyboard>/w");
        down.AddBinding("<Keyboard>/s");
        left.AddBinding("<Keyboard>/a");
        right.AddBinding("<Keyboard>/d");
        rb = GetComponent<Rigidbody2D>();

        originalMoveSpeed = moveSpeed;
        originalMaxMoveSpeed = maxMoveSpeed;
        originalFriction = friction;

        ApplyStats();
    }

    /// Enables input actions when the component becomes active.
    private void OnEnable()
    {
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();
    }

    /// Disables input actions when the component is deactivated.
    private void OnDisable()
    {
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();
    }

    /// Applies friction and processes directional input to accelerate within max speed limits.
    void Update()
    {
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, friction * Time.deltaTime);
        if (up.IsPressed() && rb.linearVelocity.y < maxMoveSpeed)
        {
            rb.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
        }

        if (down.IsPressed() && rb.linearVelocity.y > -maxMoveSpeed)
        {
            rb.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
        }

        if (left.IsPressed() && rb.linearVelocity.x > -maxMoveSpeed)
        {
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
        }

        if (right.IsPressed() && rb.linearVelocity.x < maxMoveSpeed)
        {
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
        }
    }

    /// Activates a temporary speed boost for the specified duration if not already active.
    /// <param name="duration">Duration in seconds for which the boost remains active.</param>
    public void ActivateSpeedBoost(float duration)
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            UpdateStatsForBoost();

            Invoke(nameof(DeactivateSpeedBoost), duration);
        }
    }

    /// Reverts movement speed and max speed to their original values and clears the boost flag.
    private void DeactivateSpeedBoost()
    {
        isSpeedBoosted = false;
        ApplyStats(); // Re-apply equipment stats
    }

    public void EquipWheels(WheelType wheels)
    {
        currentWheels = wheels;
        ApplyStats();
    }

    public void EquipArmor(ArmorType armor)
    {
        currentArmor = armor;
        ApplyStats();
    }

    private void ApplyStats()
    {
        float wheelSpeedMult = currentWheels ? currentWheels.speedMultiplier : 1f;
        float wheelFriction = currentWheels ? currentWheels.friction : originalFriction;

        float armorSpeedMult = currentArmor ? currentArmor.speedMultiplier : 1f;

        float totalSpeedMult = wheelSpeedMult * armorSpeedMult;

        // Base values * Equipment
        float effectiveMoveSpeed = originalMoveSpeed * totalSpeedMult;
        float effectiveMaxSpeed = originalMaxMoveSpeed * totalSpeedMult;

        if (isSpeedBoosted)
        {
            effectiveMoveSpeed *= speedBoostMultiplier;
            effectiveMaxSpeed *= speedBoostMultiplier;
        }

        moveSpeed = effectiveMoveSpeed;
        maxMoveSpeed = effectiveMaxSpeed;
        friction = wheelFriction; // "Grip Wheels: 0% sliding" implies high friction. "Turbo Treads: high sliding" implies low friction.
    }

    private void UpdateStatsForBoost()
    {
        ApplyStats(); // Since ApplyStats checks isSpeedBoosted, just call it.
    }
}
