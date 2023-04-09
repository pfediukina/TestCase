using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : Window
{
    public static Popup Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI _info;

    protected override void Awake()
    {
        base.Awake();
        if(Instance != this && Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    public void Enable(bool enable, string info, float exitTime = -1)
    {
        _info.text = info;
        Enable(enable);
        StartCoroutine(HideWindow(exitTime));
    }

    private IEnumerator HideWindow(float exitTime)
    {
        if (exitTime <= -1) yield return null;
        yield return new WaitForSeconds(exitTime);
        Enable(false);
    }
}
