using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// Displays the player's current money amount on the UI with a flash effect
public class MoneyDisplayUI : MonoBehaviour
{
    /// Reference to the MoneyManager singleton
    private MoneyManager moneyManager;

    /// UI Text element to display money (Legacy UI)
    [SerializeField] private Text moneyText;
    
    /// Prefix shown before the money amount
    [SerializeField] private string moneyPrefix = "Money: $";

    /// Color to flash when money increases
    [SerializeField] private Color flashColor = Color.green;

    /// Duration of the flash effect in seconds
    [SerializeField] private float flashDuration = 0.5f;

    /// Original color of the text
    private Color originalColor;

    /// Previous money amount (to detect changes)
    private int previousMoney;

    /// Flash timer
    private float flashTimer = 0f;

    /// Whether we're currently flashing
    private bool isFlashing = false;

    void Start()
    {
        // Get reference to MoneyManager singleton
        moneyManager = MoneyManager.Instance;

        if (moneyManager == null)
        {
            Debug.LogError("MoneyDisplayUI: MoneyManager instance not found!");
            return;
        }

        // Store original color
        if (moneyText != null)
        {
            originalColor = moneyText.color;
        }
        // Initialize previous money
        previousMoney = moneyManager.GetMoneyAmount();
    }

    void Update()
    {
        if (moneyManager == null) return;

        int currentMoney = moneyManager. GetMoneyAmount();

        // Check if money increased
        if (currentMoney > previousMoney)
        {
            TriggerFlash();
        }

        previousMoney = currentMoney;

        // Update the money display
        UpdateMoneyDisplay();

        // Handle flash animation
        if (isFlashing)
        {
            flashTimer += Time.deltaTime;

            // Calculate lerp factor (0 to 1)
            float t = flashTimer / flashDuration;

            // Lerp from flash color back to original color
            Color currentColor = Color.Lerp(flashColor, originalColor, t);
            SetTextColor(currentColor);

            // End flash when duration is reached
            if (flashTimer >= flashDuration)
            {
                isFlashing = false;
                SetTextColor(originalColor);
            }
        }
    }

    /// Updates the text UI with the current money amount
    void UpdateMoneyDisplay()
    {
        int currentMoney = moneyManager.GetMoneyAmount();
        string displayText = "$" + currentMoney;

        // Update Legacy UI Text if assigned
        if (moneyText != null)
        {
            moneyText.text = displayText;
        }
        
    }

    /// Triggers the flash effect
    void TriggerFlash()
    {
        isFlashing = true;
        flashTimer = 0f;
        SetTextColor(flashColor);
    }

    /// Sets the color of the text
    void SetTextColor(Color color)
    {
        if (moneyText != null)
        {
            moneyText.color = color;
        }
        
    }
}