using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;

public class RegAndLogin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        var data = new Dictionary<string, object>{ { "MySaveKey", "Hello World" } };
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
