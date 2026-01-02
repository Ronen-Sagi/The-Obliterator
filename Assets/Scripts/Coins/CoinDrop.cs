using UnityEngine;

public class CoinDrop : PickupDrop
{
    [SerializeField] private int coinAmount = 1;

    protected override void OnCollected(GameObject player)
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.AddMoneyAmount(coinAmount);
    }
}