using System;
using TMPro;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Handles username+password authentication.
 */
public class RegAndLoginInitialize : MonoBehaviour {
    [SerializeField] TMP_InputField usernameInputField;
    [SerializeField] TMP_InputField passwordInputField;

    [SerializeField] CloudAuthManager authManager;
    public PlayerData CurrentPlayerData { get; private set; }
    
    async void Initialize() {

        string username = usernameInputField.text;
        Debug.Log($"   username='{username}'");
        await authManager.LoadPlayerDataFromCloud();
        String levelScene = "Level " + (authManager.CurrentPlayerData.highestCompletedLevel);
        SceneManager.LoadScene(levelScene);
        
    }

    enum ButtonType { REGISTER, LOGIN };

    private async Task OnButtonClicked(ButtonType b) {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
            return;
        }
        string message;
        if (b == ButtonType.REGISTER) {
            message = await authManager.RegisterWithUsernameAndPassword(username, password);
        } else {
            message = await authManager.LoginWithUsernameAndPassword(username, password);
        }

        if (message.Contains("success")) {
            Debug.Log(message);
            if (AuthenticationService.Instance.IsSignedIn) {
                Debug.Log("Already signed in: manual initialization");
                Initialize();
            } else {
                Debug.Log("Not signed in yet: Initialize at SignedIn event");
                AuthenticationService.Instance.SignedIn += Initialize;
            }
        } else {
            Debug.LogError(message);
        }
    }

    public async void OnSignInButtonClicked() {
        await OnButtonClicked(ButtonType.LOGIN);
    }

    public async void OnSignUpButtonClicked() {
        await OnButtonClicked(ButtonType.REGISTER);
    }
    

}
