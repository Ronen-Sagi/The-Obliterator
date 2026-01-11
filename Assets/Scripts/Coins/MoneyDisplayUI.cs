using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// Displays the player's current money amount on the UI
public class MoneyDisplayUI : MonoBehaviour
{
    /// Reference to the MoneyManager singleton
    private MoneyManager moneyManager;

    /// UI Text element to display money (Legacy UI)
    [SerializeField] private Text moneyText;

    /// Prefix shown before the money amount
    [SerializeField] private string moneyPrefix = "$";

    void Start()
    {
        // Get reference to MoneyManager singleton
        moneyManager = MoneyManager.Instance;

        if (moneyManager == null)
        {
            Debug.LogError("MoneyDisplayUI: MoneyManager instance not found!");
        }
    }

    void Update()
    {
        // Update the money display every frame
        if (moneyManager != null)
        {
            UpdateMoneyDisplay();
        }
    }

    /// Updates the text UI with the current money amount
    void UpdateMoneyDisplay()
    {
        int currentMoney = moneyManager. GetMoneyAmount();

        // Update Legacy UI Text if assigned
        if (moneyText != null)
        {
            moneyText.text = moneyPrefix + currentMoney.ToString();
        }


    }
}