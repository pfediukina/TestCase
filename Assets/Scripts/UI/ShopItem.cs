using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ballName;
    [SerializeField] private TextMeshProUGUI _ballPrice;
    [SerializeField] private Image _ballImage;

    private BallData _data;
    private ShopMenu _shop;
    
    public void Init(BallData ball, ShopMenu shop)
    {
        _shop = shop;
        _data = ball;
        _ballName.text = ball.Name;
        _ballPrice.text = ball.isBought ? "<color=#00FF00>Bought</color>" : ball.Price.ToString() + " GC";
        _ballImage.color = _data.BallColor;
    }

    public void OnItemClicked()
    {
        if (_data.isBought)
        {
            Player.Instance.SetBall(_data);
            _shop.UpdateCurrentBall();
        }
        else
        {
            GetComponent<Button>().interactable = false;
            var req = new PurchaseItemRequest()
            {
                ItemId = _data.Id,
                VirtualCurrency = "GC",
                Price = _data.Price
            };
            PlayFabClientAPI.PurchaseItem(req, OnPurchaseSuccess, OnPurchaseError);
        }
    }

    private void OnPurchaseError(PlayFabError obj)
    {
        GetComponent<Button>().interactable = true;
    }

    private void OnPurchaseSuccess(PurchaseItemResult result)
    {
        GetComponent<Button>().interactable = true;
        LoadingScreen.Instance.Enable(false);
        _data.isBought = true;
        _ballPrice.text = "<color=#00FF00>Bought</color>";
        //Player.Instance.CollectCoins(_data.Price * -1);
        Player.Instance.SetBall(_data);
        _shop.UpdateCurrentBall();
    }
}
