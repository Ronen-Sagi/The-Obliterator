using UnityEngine;


/// Manages the player's money amount.
public class MoneyManager : MonoBehaviour
{
    /// The current money balance.
    [SerializeField] private int currentAmount = 100;
    //private int currentAmount;

    /// <summary>
    /// Unity lifecycle method called when the script instance is being loaded.
    /// Currently does not modify the balance, but runs earlier than <see cref="Start"/>.
    /// </summary>
    void Awake() // Changed from Start to Awake to run earlier
    {
        //currentAmount = startingMoney;
    }

    /// Adds the provided amount to the current balance.
    /// <param name="amount">Amount to add to the current balance.</param>
    public void SetMoneyAmount(int amount)
    {
        currentAmount += amount;
    }

    /// Gets the current money balance.
    /// <returns>The current balance.</returns>
    public int GetMoneyAmount()
    {
        return currentAmount;
    }

    /// Subtracts the provided amount from the current balance.
    /// <param name="amount">Amount to subtract from the current balance.</param>
    public void ReduceMoneyAmount(int amount)
    {
        currentAmount -= amount;
    }
}