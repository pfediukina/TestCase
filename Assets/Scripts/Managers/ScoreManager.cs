using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private TextMeshProUGUI _txtScoreReplay;
    //
    [SerializeField] private TextMeshProUGUI _txtCoins;
    [SerializeField] private float _scoreMultiplier = 1;
    
    public float Score { get; private set; }
    public int Coins { get; private set; }

    private void Awake()
    {
        Coins = 0;
    }

    private void OnEnable()
    {
        _ball.OnPlayerInArea += SetScore;
        Player.Instance.OnCoinCollected += OnCoinUpdates;
    }
    
    private void OnDisable()
    {
        _ball.OnPlayerInArea -= SetScore;
        Player.Instance.OnCoinCollected -= OnCoinUpdates;
    }

    private void OnCoinUpdates()
    {
        Coins += 10;
        _txtCoins.text = Coins.ToString();
    }

    private void SetScore(float value)
    {
        Score = value * _scoreMultiplier;
        _txtScore.text = Mathf.CeilToInt(Score).ToString();
        _txtScoreReplay.text = "Score\n" + Mathf.CeilToInt(Score) ;
    }
}
