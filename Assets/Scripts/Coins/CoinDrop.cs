using UnityEngine;

/// <summary>
/// Coin pickup that the player can collect to gain money. 
/// </summary>
public class CoinDrop : PickupDrop
{
    [SerializeField] private int coinAmount = 1;

    protected override void OnCollected(GameObject player)
    {
        // Find the MoneyManeger component on the player and add coins
        MoneyManager moneyManager = player.GetComponent<MoneyManager>();
        
        if (moneyManager != null)
        {
            moneyManager.SetMoneyAmount(coinAmount);
            Debug.Log($"Player collected {coinAmount} coin(s)! Total: {moneyManager.GetMoneyAmount()}");
        }
        else
        {
            Debug. LogWarning("MoneyManeger component not found on player!");
        }
    }
}