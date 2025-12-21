using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class ShopManeger : MonoBehaviour
{
    /// Reference to the MoneyManeger that tracks player's money.
    [FormerlySerializedAs("moneyManeger")] [SerializeField]
    private MoneyManager moneyManager;

    /// Text element used to show purchase confirmation messages.
    [SerializeField] private TextMeshProUGUI purchaseText;

    /// Duration that the purchaseText remains visible.
    [SerializeField] private float textDuration = 2f;

    /// Text element used to show the player's current money amount.
    [SerializeField] private TextMeshProUGUI amountText;

    /// Text elements showing each item's price in the shop UI.
    [SerializeField] private TextMeshProUGUI bulletPriceText;

    [SerializeField] private TextMeshProUGUI shieldPriceText;
    [SerializeField] private TextMeshProUGUI tiresPriceText;
    [SerializeField] private TextMeshProUGUI powerUpPriceText;

    /// Prices for the different shop items.
    [SerializeField] private int bulletPrice = 30;

    [SerializeField] private int shieldPrice = 20;
    [SerializeField] private int tiresPrice = 15;
    [SerializeField] private int powerUpPrice = 50;

    /// Internal timer for hiding the purchaseText.
    private float textTimer = 0f;

    /// Tracks whether the purchaseText is currently active.
    private bool isTextActive = false;

    /// Initializes the shop UI. Hides the purchase text and updates displayed values.
    void Start()
    {
        if (purchaseText != null)
        {
            purchaseText.gameObject.SetActive(false);
        }

        UpdateAmountText();
        UpdatePriceTexts();
    }

    /// Handles hiding timed purchase text and keeps the amount text current.
    void Update()
    {
        if (isTextActive)
        {
            textTimer -= Time.deltaTime;
            if (textTimer <= 0f)
            {
                purchaseText.gameObject.SetActive(false);
                isTextActive = false;
            }
        }

        UpdateAmountText();
    }

    /// Called when the "New Bullet" purchase button is pressed.
    /// Attempts to purchase and shows confirmation or error text.
    public void OnNewBulletPressed()
    {
        Debug.Log("bullet pressed, Money:  " + moneyManager.GetMoneyAmount());
        if (moneyManager.GetMoneyAmount() >= bulletPrice)
        {
            moneyManager.ReduceMoneyAmount(bulletPrice);
            ShowPurchaseText("New bullet purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new bullet");
        }
    }

    /// Called when the "New Shield" purchase button is pressed.
    /// Attempts to purchase and shows confirmation or error text.
    public void OnNewShieldPressed()
    {
        Debug.Log("shield pressed, Money:  " + moneyManager.GetMoneyAmount());
        if (moneyManager.GetMoneyAmount() >= shieldPrice)
        {
            moneyManager.ReduceMoneyAmount(shieldPrice);
            ShowPurchaseText("New shield purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new shield");
        }
    }

    /// Called when the "New Tires" purchase button is pressed.
    /// Attempts to purchase and shows confirmation or error text.
    public void OnNewTirePressed()
    {
        Debug.Log("tires pressed, Money:  " + moneyManager.GetMoneyAmount());
        if (moneyManager.GetMoneyAmount() >= tiresPrice)
        {
            moneyManager.ReduceMoneyAmount(tiresPrice);
            ShowPurchaseText("New tires purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new tires");
        }
    }

    /// Called when the "New Power-Up" purchase button is pressed.
    /// Attempts to purchase and shows confirmation or error text.
    public void OnNewPowerUpPressed()
    {
        Debug.Log("power-up pressed, Money:  " + moneyManager.GetMoneyAmount());
        if (moneyManager.GetMoneyAmount() >= powerUpPrice)
        {
            moneyManager.ReduceMoneyAmount(powerUpPrice);
            ShowPurchaseText("New power-up purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new power-up");
        }
    }

    /// Displays a message in the purchaseText UI element for the configured duration.
    /// If purchaseText is null, the call is ignored.
    /// <param name="message">Message to display (confirmation or error).</param>
    private void ShowPurchaseText(string message)
    {
        if (purchaseText != null)
        {
            purchaseText.text = message;
            purchaseText.gameObject.SetActive(true);
            textTimer = textDuration;
            isTextActive = true;
        }
    }

    /// Updates the amountText UI element to show the current money amount.
    /// Safely checks for null references.
    public void UpdateAmountText()
    {
        if (amountText != null && moneyManager != null)
        {
            amountText.text = "You have: " + moneyManager.GetMoneyAmount().ToString() + "$";
        }
    }

    /// Updates all price text UI elements to reflect the configured item prices.
    /// Safely checks for null references.
    public void UpdatePriceTexts()
    {
        if (bulletPriceText != null)
        {
            bulletPriceText.text = bulletPrice.ToString() + "$";
        }

        if (shieldPriceText != null)
        {
            shieldPriceText.text = shieldPrice.ToString() + "$";
        }

        if (tiresPriceText != null)
        {
            tiresPriceText.text = tiresPrice.ToString() + "$";
        }

        if (powerUpPriceText != null)
        {
            powerUpPriceText.text = powerUpPrice.ToString() + "$";
        }
    }
}