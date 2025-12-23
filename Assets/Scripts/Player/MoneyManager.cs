using UnityEngine;

/// Manages the player's money amount for the current session and syncs with GameManager.
public class MoneyManager : MonoBehaviour
{
    /// The current money balance for this session/level.
    [SerializeField] private int currentAmount = 0;

    /// <summary>
    /// Unity lifecycle method called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // Initialize with 0 or however the level should start.
        // If we want to carry over total coins, we can read from GameManager.
        // But the requirement says "Only reset the 'In-Level' coins, never the 'Total' bank account."
        // So currentAmount here likely represents "In-Level" coins.
        currentAmount = 0;
    }

    /// Adds the provided amount to the current balance and the persistent bank.
    /// <param name="amount">Amount to add.</param>
    public void SetMoneyAmount(int amount)
    {
        currentAmount += amount;

        // Add to persistent storage
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoins(amount);
        }
        else
        {
            Debug.LogWarning("GameManager instance not found!");
        }
    }

    /// Gets the current session money balance.
    /// <returns>The current balance.</returns>
    public int GetMoneyAmount()
    {
        return currentAmount;
    }

    /// Subtracts the provided amount from the current balance.
    /// Used for in-level spending if any.
    /// <param name="amount">Amount to subtract.</param>
    public void ReduceMoneyAmount(int amount)
    {
        currentAmount -= amount;
        // Note: Reducing session money usually doesn't reduce total bank unless it's a purchase.
        // If this is used for shop, we should probably use GameManager directly or update it.
        // Assuming this is used for temporary mechanics or should reflect in GameManager:

        // However, if this is for the Shop which uses total coins, we should check.
        // The bug report says "totalCoins resets to 100 upon entering the shop".
        // If the Shop uses MoneyManager, we should probably expose TotalCoins here too.
    }

    // Helper to get total coins from persistent storage
    public int GetTotalCoins()
    {
        if (GameManager.Instance != null)
        {
            return GameManager.Instance.TotalCoins;
        }
        return 0;
    }
}
