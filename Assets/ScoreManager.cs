using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private TextMeshProUGUI _txtScore;
    
    private float _score;

    private void OnEnable()
    {
        _ball.OnPlayerInArea += SetScore;
    }

    private void SetScore(float value)
    {
        _score += value;
        _txtScore.text = Mathf.CeilToInt(_score).ToString();
    }
}
