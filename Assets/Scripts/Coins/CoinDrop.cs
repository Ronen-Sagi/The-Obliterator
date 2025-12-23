using UnityEngine;

/// Coin pickup that the player can collect to gain money.
/// Inherits from <see cref="PickupDrop"/> and applies a fixed coin amount to the player's <see cref="MoneyManager"/> when collected.
public class CoinDrop : PickupDrop
{
    /// The number of coins got when collected.
    [SerializeField] private int coinAmount = 1;

    /// Called when the pickup is collected by a player.
    /// Attempts to find a <see cref="MoneyManager"/> component on the provided <paramref name="player"/>.
    /// If found, sets the player's money amount using <see cref="MoneyManager.SetMoneyAmount(int)"/>.
    /// <param name="player">The player <see cref="GameObject"/> that collected this pickup.</param>
    protected override void OnCollected(GameObject player)
    {
        // Find the MoneyManager component on the player and add coins
        MoneyManager moneyManager = player.GetComponent<MoneyManager>();

        if (moneyManager != null)
        {
            moneyManager.SetMoneyAmount(coinAmount);
        }
        else
        {
            // Fallback: Try to add directly to GameManager if MoneyManager isn't on player
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCoins(coinAmount);
            }
        }
    }
}
