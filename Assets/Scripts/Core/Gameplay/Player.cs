using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Action OnCoinCollected;
    public Action OnShopLoaded;

    public bool IsShopLoaded { get; private set; }
    public int Highscore { get; private set; }
    public int GC { get; private set; }
    public BallData CurrentBallData { get; private set; }
    public HashSet<BallData> Balls => _balls;
    
    private HashSet<BallData> _balls = new HashSet<BallData>();
    private HashSet<ItemInstance> _bought = new HashSet<ItemInstance>();
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        DontDestroyOnLoad(this);
    }
    
    private void SetHighscore(int score)
    {
        Highscore = score;
    }

    public void SetBall(BallData data)
    {
        CurrentBallData = data;
    }
    
    public void GetPlayerData()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        GetHighscore();
        GetShop();
    }

    private void GetHighscore()
    {
        GetPlayerStatisticsRequest req = new GetPlayerStatisticsRequest()
        {
            StatisticNames = new List<string>
            {
                "highscore"
            }
        };
        PlayFabClientAPI.GetPlayerStatistics(req, OnStatisticsSuccess, error => Debug.LogError(error));
    }

    private void OnStatisticsSuccess(GetPlayerStatisticsResult result)
    {
        if(result.Statistics.Count != 0)
            SetHighscore(result.Statistics[0].Value);
        else
            SetHighscore(0);
    }

    private void GetInventory()
    {
        _bought.Clear();
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnInvSuccess, null);
    }

    private void CompareItems()
    {
        foreach (var ball in _balls)
        {
            var item = _bought.Where(p => p.ItemId == ball.Id).FirstOrDefault();
            
            if (item != null)
            {
                ball.isBought = true;
            }
        }

        CurrentBallData ??= _balls.Where(p => p.isBought == true).FirstOrDefault();
        Debug.Log(CurrentBallData);
        if (!IsShopLoaded)
        {
            OnShopLoaded?.Invoke();
            LoadingScreen.Instance.Enable(false);
        }
        IsShopLoaded = true;
    }

    private void OnInvSuccess(GetUserInventoryResult result)
    {
        foreach (var item in result.Inventory)
        {
            if (item.ItemClass == "Ball")
            {
                _bought.Add(item);
            }
        }

        GC = result.VirtualCurrency["GC"];
        CompareItems();
    }
    
    private void GetShop()
    {
        var req = new GetCatalogItemsRequest()
        {
            CatalogVersion = "Balls"
        };
        PlayFabClientAPI.GetCatalogItems(req, OnGetCatalogSuccess, null);
    }

    private void OnGetCatalogSuccess(GetCatalogItemsResult result)
    {
        _balls.Clear();
        foreach (var item in result.Catalog)
        {
            var ballData = new BallData();
            ballData.Id = item.ItemId;
            ballData.Name = item.DisplayName;
            ballData.DeserializeCustomData(item.CustomData);
            ballData.Price = (int)item.VirtualCurrencyPrices["GC"];
            ballData.isBought = false;
            _balls.Add(ballData);
        }
        
        GetInventory();
    }

    public void SaveHighscore(int highscore)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;
        
        UpdatePlayerStatisticsRequest req = new UpdatePlayerStatisticsRequest()
        {

            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "highscore",
                    Value = highscore
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(req, ctx => SetHighscore(highscore), error => Debug.LogError(error));
    }

    public void SaveCoins(int coins)
    {
        if(!PlayFabClientAPI.IsClientLoggedIn()) return;
        GC += coins;
        AddUserVirtualCurrencyRequest req = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "GC",
            Amount = coins
        };
        PlayFabClientAPI.AddUserVirtualCurrency(req, OnAddingGCSuccess, null);
    }

    private void OnAddingGCSuccess(ModifyUserVirtualCurrencyResult res)
    {
        
    }

    public void CollectCoins()
    {
        OnCoinCollected?.Invoke();
    }
}