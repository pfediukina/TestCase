using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PlayfabLogin _pf;
    
    [SerializeField] private GameObject _login;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _shop;

    private static bool _isLogged = false;

    private void Awake()
    {
        if (!_isLogged)
            _login.SetActive(true);
        else
            ShowMainMenu();
    }

    private void OnEnable()
    {
        _pf.OnSuccessLogin += () =>
        {
            ShowMainMenu();
            _isLogged = true;
        };
    }

    public void ShowMainMenu()
    {
        _login.SetActive(false);
        _shop.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void ShowLogin()
    {
        _shop.SetActive(false);
        _mainMenu.SetActive(false);
        _login.SetActive(true); 
    }
    
    public void ShowShopMenu()
    {
        _login.SetActive(false);
        _mainMenu.SetActive(false);
        _shop.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
