using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : Window
{
    [SerializeField] private TextMeshProUGUI _playerCurrency;
    [SerializeField] private ShopItem _item;
    [SerializeField] private Transform _scrollContent;
    [SerializeField] private Image _currentBall;

    private void OnEnable()
    {
        Player.Instance.OnShopLoaded += ShowShop;
    }

    private void OnDisable()
    {
        Player.Instance.OnShopLoaded -= ShowShop;
    }

    public void ShowShop()
    {
        _currentBall.color = Player.Instance.CurrentBallData.BallColor;
        _playerCurrency.text = Player.Instance.GC.ToString();
        ResetChildren();
        foreach (var ball in Player.Instance.Balls)
        {
            var item = Instantiate(_item, _scrollContent);
            item.Init(ball, this);
        }
        
        LoadingScreen.Instance.Enable(false);
    }

    private void ResetChildren()
    {
        int childs = _scrollContent.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            Debug.Log("Destroyed");
            Destroy(_scrollContent.GetChild(i).gameObject);
        }
    }

    public void UpdateCurrentBall()
    {
        _currentBall.color = Player.Instance.CurrentBallData.BallColor;
        _playerCurrency.text = Player.Instance.GC.ToString();
    }
}
 