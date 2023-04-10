using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PlayFabLogin _pf;
    
    [SerializeField] private Window _login;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private ShopMenu _shop;

    private static bool _isLogged = false;

    private void Awake()
    {
        if (!_isLogged)
            _login.Enable(true);
        else
            ShowMainMenu();
    }

    private void OnEnable()
    {
        Player.Instance.OnShopLoaded += SuccessLogin;
    }

    private void SuccessLogin()
    {
        ShowMainMenu();
        LoadingScreen.Instance.Enable(false);
        _isLogged = true;
    }

    public void ShowMainMenu()
    {
        //LoadingScreen.Instance.Enable(false); 
        _login.Enable(false);
        _shop.Enable(false);
        _mainMenu.SetActive(true);
    }
    
    public void ShowShopMenu()
    {
        _login.Enable(false);
        _mainMenu.SetActive(false);
        _shop.Enable(true);
        LoadingScreen.Instance.Enable(true); 
        if(Player.Instance.IsShopLoaded)
            _shop.ShowShop();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
