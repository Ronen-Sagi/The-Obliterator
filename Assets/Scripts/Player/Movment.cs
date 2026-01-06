using UnityEngine;
using UnityEngine.InputSystem;

/// Handles 2D player movement using the Unity Input System with WASD bindings,
/// applying impulse forces, friction-based deceleration and a temporary speed boost.
/// More kind of boost will be added later.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]
public class Movment : MonoBehaviour
{
    private PlayerStats ps;
    
    /// Input actions for moving 
    [SerializeField] private InputAction up = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction down = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction left = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction right = new InputAction(type: InputActionType.Button);

    /// Impulse force applied per input press to accelerate the player.
    private float moveSpeed = 5f;

    /// Maximum allowed linear speed.
    [SerializeField] private float maxMoveSpeed = 5f;

    /// Friction factor used to lerp velocity toward zero each frame.
    private float friction = 5f;

    /// Multiplier applied to speed and max speed during a speed boost.
    [SerializeField] private float speedBoostMultiplier = 2f;

    /// Cached reference to the <see cref="Rigidbody2D"/> controlling physics-based movement.
    private Rigidbody2D rb;

    /// Original configured move speed before any boosts.
    private float originalMoveSpeed;

    /// Original configured max move speed before any boosts.
    private float originalMaxMoveSpeed;

    /// Indicates whether a speed boost is currently active.
    private bool isSpeedBoosted = false;

    /// Initializes input bindings and caches original speed values.
    void Awake()
    {
        up.AddBinding("<Keyboard>/w");
        down.AddBinding("<Keyboard>/s");
        left.AddBinding("<Keyboard>/a");
        right.AddBinding("<Keyboard>/d");

        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<PlayerStats>();

        if (ps == null)
        {
            Debug.LogError("Movment: Missing PlayerStats component on this GameObject.", this);
            enabled = false;
            return;
        }
        
        SampleStats();

        originalMoveSpeed = moveSpeed;
        originalMaxMoveSpeed = maxMoveSpeed;
    }

    private void SampleStats()
    {
        moveSpeed = ps.MoveSpeed;
        friction = ps.Friction;
    }

    /// Enables input actions when the component becomes active.
    private void OnEnable()
    {
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();

        if (ps != null)
        {
            ps.OnStatsChanged += SampleStats;
        }
    }

    /// Disables input actions when the component is deactivated.
    private void OnDisable()
    {
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();
        if (ps != null)
        {
            ps.OnStatsChanged -= SampleStats;
        }
    }

    /// Applies friction and processes directional input to accelerate within max speed limits.
    void Update()
    {
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, ps.Friction * Time.deltaTime);
        if (up.IsPressed() && rb.linearVelocity.y < maxMoveSpeed)
        {
            rb.AddForce(Vector2.up * ps.MoveSpeed, ForceMode2D.Impulse);
        }

        if (down.IsPressed() && rb.linearVelocity.y > -maxMoveSpeed)
        {
            rb.AddForce(Vector2.down * ps.MoveSpeed, ForceMode2D.Impulse);
        }

        if (left.IsPressed() && rb.linearVelocity.x > -maxMoveSpeed)
        {
            rb.AddForce(Vector2.left * ps.MoveSpeed, ForceMode2D.Impulse);
        }

        if (right.IsPressed() && rb.linearVelocity.x < maxMoveSpeed)
        {
            rb.AddForce(Vector2.right * ps.MoveSpeed, ForceMode2D.Impulse);
        }
    }

    /// Activates a temporary speed boost for the specified duration if not already active.
    /// <param name="duration">Duration in seconds for which the boost remains active.</param>
    public void ActivateSpeedBoost(float duration)
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            moveSpeed = originalMoveSpeed * speedBoostMultiplier;
            maxMoveSpeed = originalMaxMoveSpeed * speedBoostMultiplier;

            Invoke(nameof(DeactivateSpeedBoost), duration);
        }
    }

    /// Reverts movement speed and max speed to their original values and clears the boost flag.
    private void DeactivateSpeedBoost()
    {
        moveSpeed = originalMoveSpeed;
        maxMoveSpeed = originalMaxMoveSpeed;
        isSpeedBoosted = false;
    }
}