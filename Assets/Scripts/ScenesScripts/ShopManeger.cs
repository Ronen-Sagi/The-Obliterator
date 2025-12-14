using UnityEngine;
using TMPro;

public class ShopManeger : MonoBehaviour
{
    [SerializeField] private MoneyManeger moneyManeger;
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private float textDuration = 2f;
    
    [SerializeField] private TextMeshProUGUI amountText;

    [SerializeField] private int bulletPrice = 30;
    [SerializeField] private int shieldPrice = 20;
    [SerializeField] private int tiresPrice = 15;
    [SerializeField] private int powerUpPrice = 50;

    private float textTimer = 0f;

    private bool isTextActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (purchaseText != null)
        {
            purchaseText.gameObject.SetActive(false);
        }

        UpdateAmountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTextActive)
        {
            textTimer -= Time.deltaTime;
            if (textTimer <= 0f)
            {
                purchaseText.gameObject.SetActive(false);
                isTextActive = false;
            }
        }

        UpdateAmountText();
    }

    public void OnNewBulletPressed()
    {
        Debug.Log("bullet pressed, Money:  " + moneyManeger.GetMoneyAmount());
        if (moneyManeger.GetMoneyAmount() >= bulletPrice)
        {
            moneyManeger.ReduceMoneyAmount(bulletPrice);
            ShowPurchaseText("New bullet purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new bullet");
        }
    }

    public void OnNewShieldPressed()
    {
        Debug.Log("shield pressed, Money:  " + moneyManeger.GetMoneyAmount());
        if (moneyManeger.GetMoneyAmount() >= shieldPrice)
        {
            moneyManeger.ReduceMoneyAmount(shieldPrice);
            ShowPurchaseText("New shield purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new shield");
        }
    }

    public void OnNewTirePressed()
    {
        Debug.Log("tires pressed, Money:  " + moneyManeger.GetMoneyAmount());
        if (moneyManeger.GetMoneyAmount() >= tiresPrice)
        {
            moneyManeger.ReduceMoneyAmount(tiresPrice);
            ShowPurchaseText("New tires purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new tires");
        }
    }

    public void OnNewPowerUpPressed()
    {
        Debug.Log("power-up pressed, Money:  " + moneyManeger.GetMoneyAmount());
        if (moneyManeger.GetMoneyAmount() >= powerUpPrice)
        {
            moneyManeger.ReduceMoneyAmount(powerUpPrice);
            ShowPurchaseText("New power-up purchased");
        }
        else
        {
            ShowPurchaseText("Not enough money for new power-up");
        }
    }

    /// Displays the purchase confirmation text for a short duration. 
    private void ShowPurchaseText(string message)
    {
        if (purchaseText != null)
        {
            purchaseText.text = message;
            purchaseText.gameObject.SetActive(true);
            textTimer = textDuration;
            isTextActive = true;
        }
    }
    
    public void UpdateAmountText()
    {
        if (amountText != null && moneyManeger != null)
        {
            amountText.text = "You have: " + moneyManeger.GetMoneyAmount().ToString() + "$";
        }
    }
}