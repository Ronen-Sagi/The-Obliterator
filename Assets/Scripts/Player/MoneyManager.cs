using UnityEngine;


/// Manages the player's money amount.
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance {get ; private set;}
    
    [SerializeField] private int startingAmount = 100;
    private int currentAmount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        currentAmount = startingAmount;

    }


    /// Adds the provided amount to the current balance.
    /// <param name="amount">Amount to add to the current balance.</param>
    public void AddMoneyAmount(int amount)
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