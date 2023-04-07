using System;
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
        LoadingScreen.Instance.Enable(false);
        Debug.Log("Login successful!");
        OnSuccessLogin?.Invoke();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        LoadingScreen.Instance.Enable(false);
        Debug.LogError("Login failed: " + error.GenerateErrorReport());
        Popup.Instance.Enable(true,error.GenerateErrorReport(), 5);
        OnFailtureLogin?.Invoke();
    }
}