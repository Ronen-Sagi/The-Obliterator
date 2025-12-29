using System;

/// <summary>
/// Represents the player's save data stored in Unity Cloud Save.
/// </summary>
[Serializable]
public class PlayerData
{
    /// <summary>
    /// The player's username.
    /// </summary>
    public string username;

    /// <summary>
    /// The highest level the player has completed (0 = none).
    /// </summary>
    public int highestCompletedLevel;

    /// <summary>
    /// The last level the player completed. 
    /// </summary>
    public int lastCompletedLevel;

    /// <summary>
    /// The player's current money/currency amount.
    /// </summary>
    public int money;

    /// <summary>
    /// Constructor to initialize default player data.
    /// </summary>
    public PlayerData()
    {
        username = "";
        highestCompletedLevel = 0;
        lastCompletedLevel = 0;
        money = 0;
    }

    /// <summary>
    /// Constructor with parameters. 
    /// </summary>
    public PlayerData(string username, int highestLevel, int lastLevel, int money)
    {
        this.username = username;
        this.highestCompletedLevel = highestLevel;
        this.lastCompletedLevel = lastLevel;
        this.money = money;
    }
}