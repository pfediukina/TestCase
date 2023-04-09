using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreen : Window
{
    public static LoadingScreen Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if(Instance != this && Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }
}
