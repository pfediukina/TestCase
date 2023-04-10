using System;
using UnityEngine;

public class Coin : MonoBehaviour, ILootable
{
    [SerializeField] private int _coins;
    private CoinFactory _factory;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ball"))
        {
            OnLoot();
        }
    }

    public void OnLoot()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(SoundTag.COLLECT_SOUND);
        Player.Instance.CollectCoins();
    }
}