using UnityEngine;

public class MoneyManeger : MonoBehaviour
{
    [SerializeField] private int currentAmount = 100;
    //private int currentAmount;

    void Awake() // Changed from Start to Awake to run earlier
    {
        //currentAmount = startingMoney;
    }

    public void SetMoneyAmount(int amount)
    {
        currentAmount += amount;
    }

    public int GetMoneyAmount()
    {
        return currentAmount;
    }

    public void ReduceMoneyAmount(int amount)
    {
        currentAmount -= amount;
    }
}