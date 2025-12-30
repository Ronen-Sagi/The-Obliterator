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
    /// The player's current money/currency amount.
    /// </summary>
    public int money;

    /// <summary>
    /// Constructor to initialize default player data.
    /// </summary>
    public PlayerData()
    {
        username = "";
        highestCompletedLevel = 1;
        money = 0;
    }

    /// <summary>
    /// Constructor with parameters. 
    /// </summary>
    public PlayerData(string username, int highestLevel, int money)
    {
        this.username = username;
        this.highestCompletedLevel = highestLevel;
        this.money = money;
    }
    
    public void SetHighestCompletedLevel(int level)
    {
        if (level > highestCompletedLevel)
        {
            highestCompletedLevel = level;
        }
    }
}