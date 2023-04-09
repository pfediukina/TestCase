using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager _score;
    [SerializeField] private DefeatArea _area;
    
    [Header("UI")]
    [SerializeField] private GameObject _replayScreen;

    private void Awake()
    {
        _replayScreen.SetActive(false);
    }
    private void OnEnable()
    {
        _area.OnDefeat += Defeat;
    }
    
    private void OnDisable()
    {
        _area.OnDefeat -= Defeat;
    }

    private void Defeat()
    {
        SoundManager.Instance.PlaySound(SoundTag.LOSE_SOUND);
        _replayScreen.SetActive(true);
        if (_score.Score > Player.Instance.Highscore)
        {
            Player.Instance.SaveHighscore(Mathf.CeilToInt(_score.Score));
        }
        Player.Instance.SaveCoins(_score.Coins);
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
    
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
