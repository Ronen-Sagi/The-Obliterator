using UnityEngine;

public class CoinDrop : PickupDrop
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private int coinAmount = 1;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnCollected(GameObject player)
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.AddMoneyAmount(coinAmount);

            if (coinSound != null)
                AudioSource.PlayClipAtPoint(coinSound, transform.position, 1f);
        }
    }
}