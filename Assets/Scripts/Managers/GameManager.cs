using UnityEngine;

/// <summary>
/// GameManager singleton that handles persistent data like Total Coins and Total XP.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Persistent data keys
    private const string TOTAL_COINS_KEY = "TotalCoins";
    private const string TOTAL_XP_KEY = "TotalXP";

    // Runtime data
    public int TotalCoins { get; private set; }
    public int TotalXP { get; private set; }

    // Current session data (optional, depends on needs)
    public int SessionCoins { get; private set; }
    public int SessionXP { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    /// <summary>
    /// Loads persistent data from PlayerPrefs.
    /// </summary>
    private void LoadData()
    {
        TotalCoins = PlayerPrefs.GetInt(TOTAL_COINS_KEY, 0); // Default to 0, not 100 as per bug report logic (resetting to 100 was the bug)
        TotalXP = PlayerPrefs.GetInt(TOTAL_XP_KEY, 0);
    }

    /// <summary>
    /// Saves persistent data to PlayerPrefs.
    /// </summary>
    public void SaveData()
    {
        PlayerPrefs.SetInt(TOTAL_COINS_KEY, TotalCoins);
        PlayerPrefs.SetInt(TOTAL_XP_KEY, TotalXP);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Adds coins to the persistent bank.
    /// </summary>
    /// <param name="amount">Amount to add.</param>
    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        SessionCoins += amount;
        SaveData();
    }

    /// <summary>
    /// Adds XP to the persistent tracker (if needed) or session tracker.
    /// </summary>
    /// <param name="amount">Amount to add.</param>
    public void AddXP(int amount)
    {
        TotalXP += amount; // If XP is persistent
        SessionXP += amount;
        SaveData();

        // Notify ExperienceManager if it exists (for Mid-Run Evolution)
        // ExperienceManager.Instance.AddXP(amount);
    }

    /// <summary>
    /// Gets the current total coins.
    /// </summary>
    /// <returns>Total coins.</returns>
    public int GetTotalCoins()
    {
        return TotalCoins;
    }

    // Debug method to reset data
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        TotalCoins = 0;
        TotalXP = 0;
        SessionCoins = 0;
        SessionXP = 0;
    }
}
