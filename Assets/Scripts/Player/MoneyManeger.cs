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

    public void AddMoney(int amount)
    {
        currentAmount += amount;
    }
}
