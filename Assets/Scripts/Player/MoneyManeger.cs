using UnityEngine;

public class MoneyManeger : MonoBehaviour
{
    [SerializeField] private int startingMoney = 50;
    private int currentAmount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmount = startingMoney;
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
