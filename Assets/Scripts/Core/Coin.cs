using System;
using UnityEngine;

public class Coin : MonoBehaviour, ILootable
{
    [SerializeField] private int _coins;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ball"))
        {
            OnLoot();
        }
    }

    public void OnLoot()
    {
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(SoundTag.COLLECT_SOUND);
        Player.Instance.CollectCoins();
    }
}