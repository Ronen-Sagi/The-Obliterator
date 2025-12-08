using UnityEngine;
using UnityEngine. InputSystem;

public class Movment : MonoBehaviour
{
    [SerializeField] private InputAction up = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction down = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction left = new InputAction(type: InputActionType.Button);
    [SerializeField] private InputAction right = new InputAction(type: InputActionType.Button);
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float friction = 5f;
    [SerializeField] private float speedBoostMultiplier = 2f;

    private Rigidbody2D rb;
    private float originalMoveSpeed;
    private float originalMaxMoveSpeed;
    private bool isSpeedBoosted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        up.AddBinding("<Keyboard>/w");
        down. AddBinding("<Keyboard>/s");
        left.AddBinding("<Keyboard>/a");
        right. AddBinding("<Keyboard>/d");
        rb = GetComponent<Rigidbody2D>();
        
        originalMoveSpeed = moveSpeed;
        originalMaxMoveSpeed = maxMoveSpeed;
    }

    private void OnEnable()
    {
        up.Enable();
        down. Enable();
        left.Enable();
        right.Enable();
    }

    private void OnDisable()
    {
        up. Disable();
        down. Disable();
        left.Disable();
        right.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        rb. linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2. zero, friction * Time.deltaTime);
        if (up.IsPressed() && rb.linearVelocity.y < maxMoveSpeed)
        {
            rb.AddForce(Vector2. up * moveSpeed, ForceMode2D.Impulse);
        }

        if (down. IsPressed() && rb.linearVelocity.y > -maxMoveSpeed)
        {
            rb.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
        }

        if (left.IsPressed() && rb. linearVelocity.x > -maxMoveSpeed)
        {
            rb.AddForce(Vector2.left * moveSpeed, ForceMode2D. Impulse);
        }

        if (right.IsPressed() && rb.linearVelocity. x < maxMoveSpeed)
        {
            rb.AddForce(Vector2.right * moveSpeed, ForceMode2D. Impulse);
        }
    }

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

    private void DeactivateSpeedBoost()
    {
        moveSpeed = originalMoveSpeed;
        maxMoveSpeed = originalMaxMoveSpeed;
        isSpeedBoosted = false;
    }
}