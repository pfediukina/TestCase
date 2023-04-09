using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabLogin : MonoBehaviour
{
    public Action OnSuccessLogin;
    public Action OnFailtureLogin;
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
        LoadingScreen.Instance.Enable(false);
        OnSuccessLogin?.Invoke();
        Player.Instance.GetPlayerData();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        LoadingScreen.Instance.Enable(false);
        Debug.LogError("Login failed: " + error.GenerateErrorReport());
        Popup.Instance.Enable(true,error.GenerateErrorReport(), 5);
        OnFailtureLogin?.Invoke();
    }
}