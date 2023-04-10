using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    public void LoginAsGuest()
    {
        LoadingScreen.Instance.Enable(true);
        var request = new LoginWithCustomIDRequest
        {
            CreateAccount = true,
            CustomId = SystemInfo.deviceUniqueIdentifier
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful!");
        Player.Instance.GetPlayerData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        LoadingScreen.Instance.Enable(false);
        Debug.LogError("Login failed: " + error.GenerateErrorReport());
        Popup.Instance.Enable(true,error.GenerateErrorReport(), 5);
    }
}