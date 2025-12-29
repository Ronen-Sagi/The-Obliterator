using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

/**
 * Handles Unity Gaming Services initialization, username+password authentication,
 * and Cloud Save for player data (level progress and money).
 */
public class CloudAuthManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance for easy access from other scripts.
    /// </summary>
    public static CloudAuthManager Instance { get; private set; }

    /// <summary>
    /// The currently logged-in player's data.
    /// </summary>
    public PlayerData CurrentPlayerData { get; private set; }

    /// <summary>
    /// Key used to store player data in Unity Cloud Save.
    /// </summary>
    private const string PLAYER_DATA_KEY = "PlayerSaveData";

    void Awake()
    {
        // Singleton pattern - only one CloudAuthManager should exist
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Initializing the Unity Services SDK
    async void Start()
    {
        Debug.Log("CloudAuthManager Start - Initializing Unity Services");
        await UnityServices.InitializeAsync();

        if (AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log($"Player is already signed in as: {AuthenticationService.Instance. PlayerId}");
            await LoadPlayerDataFromCloud();
        }
        else
        {
            Debug.Log("Player is not signed in yet - waiting for sign-in");
        }
    }

    /**
     * Sign up a new user with username and password. 
     * Creates new player data with defaults. 
     * Return the success/error message. 
     */
    public async Task<string> RegisterWithUsernameAndPassword(string username, string password)
    {
        try
        {
            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return "Username and password cannot be empty! ";
            }

            if (password.Length < 8)
            {
                return "Password must be at least 8 characters! ";
            }

            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log($"Register successful! Player ID: {AuthenticationService.Instance.PlayerId}");

            // Create new player data with defaults
            CurrentPlayerData = new PlayerData(username, 0, 0, 0);
            await SavePlayerDataToCloud();

            return "Registration successful! ";
        }
        catch (AuthenticationException ex)
        {
            return $"Register failed:  {ex.Message}";
        }
        catch (RequestFailedException ex)
        {
            return $"Register request failed: {ex.Message}";
        }
    }

    /**
     * Sign in an existing user with username and password.
     * Loads their saved data from the cloud.
     * Return the success/error message.
     */
    public async Task<string> LoginWithUsernameAndPassword(string username, string password)
    {
        try
        {
            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return "Username and password cannot be empty!";
            }

            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log($"Login successful! Player ID:  {AuthenticationService.Instance.PlayerId}");

            // Load player data from cloud
            await LoadPlayerDataFromCloud();

            return "Login successful!";
        }
        catch (AuthenticationException ex)
        {
            return $"Login failed: {ex.Message}";
        }
        catch (RequestFailedException ex)
        {
            return $"Login request failed: {ex.Message}";
        }
    }

    /**
     * Sign out the current user
     */
    public void SignOut()
    {
        AuthenticationService.Instance.SignOut();
        CurrentPlayerData = null;
        Debug.Log("Player signed out");
    }

    /**
     * Saves the current player data to Unity Cloud Save. 
     */
    public async Task SavePlayerDataToCloud()
    {
        try
        {
            if (CurrentPlayerData == null)
            {
                Debug.LogError("No player data to save!");
                return;
            }

            // Convert PlayerData to JSON string
            string jsonData = JsonUtility.ToJson(CurrentPlayerData);

            // Create dictionary for Cloud Save
            var data = new Dictionary<string, object>
            {
                { PLAYER_DATA_KEY, jsonData }
            };

            // Save to cloud
            await CloudSaveService.Instance.Data.Player. SaveAsync(data);
            Debug.Log($"Player data saved to cloud: {jsonData}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save player data: {e.Message}");
        }
    }

    /**
     * Loads player data from Unity Cloud Save. 
     */
    public async Task LoadPlayerDataFromCloud()
    {
        try
        {
            // Load data from cloud
            var savedData = await CloudSaveService. Instance.Data.Player.LoadAsync(
                new HashSet<string> { PLAYER_DATA_KEY }
            );

            if (savedData.ContainsKey(PLAYER_DATA_KEY))
            {
                // Extract the string value from the Item object
                string jsonData = savedData[PLAYER_DATA_KEY].Value.GetAs<string>();
                CurrentPlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log($"Player data loaded from cloud:  {jsonData}");
            }
            else
            {
                Debug.LogWarning("No saved player data found in cloud.  Creating new data.");
                CurrentPlayerData = new PlayerData();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load player data: {e.Message}");
            CurrentPlayerData = new PlayerData();
        }
    }

    /**
     * Updates player progress (level completion) and saves to cloud.
     */
    public async Task UpdatePlayerProgress(int completedLevel)
    {
        if (CurrentPlayerData == null) return;

        CurrentPlayerData.lastCompletedLevel = completedLevel;

        if (completedLevel > CurrentPlayerData.highestCompletedLevel)
        {
            CurrentPlayerData.highestCompletedLevel = completedLevel;
        }

        await SavePlayerDataToCloud();
        Debug.Log($"Player progress updated: Level {completedLevel} completed.");
    }

    /**
     * Updates player money and saves to cloud.  
     */
    public async Task UpdatePlayerMoney(int amount)
    {
        if (CurrentPlayerData == null) return;

        CurrentPlayerData.money += amount;
        await SavePlayerDataToCloud();
        Debug.Log($"Player money updated: {CurrentPlayerData.money}");
    }
}