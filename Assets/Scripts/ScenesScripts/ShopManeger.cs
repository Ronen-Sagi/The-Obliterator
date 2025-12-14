using UnityEngine;
using TMPro;

public class ShopManeger : MonoBehaviour
{
    [SerializeField] private MoneyManeger moneyManeger;
    [SerializeField] private TextMeshProUGUI purchaseText;
    [SerializeField] private float textDuration = 2f;

    private float textTimer = 0f;

    private bool isTextActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (purchaseText != null)
        {
            purchaseText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTextActive)
        {
            textTimer -= Time.deltaTime;
            if (textTimer <= 0f)
            {
                purchaseText.gameObject.SetActive(false);
                isTextActive = false;
            }
        }
    }

    public void OnNewBulletPressed()
    {
        if (moneyManeger.GetMoneyAmount() >= 20)
        {
            moneyManeger.ReduceMoneyAmount(20);
            ShowPurchaseText("New bullet purchased");
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
}