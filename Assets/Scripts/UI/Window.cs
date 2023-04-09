using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.Internal;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Window : MonoBehaviour
{
    private CanvasGroup _canvas;
    
    protected virtual void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    public virtual void Enable(bool enable)
    {
        if (enable) ShowScreen();
        else HideScreen();
    }
    
    private void ShowScreen()
    {
        if(_canvas == null) return;
        _canvas.alpha = 1;
        _canvas.blocksRaycasts = true;
    }

    private void HideScreen()
    {
        _canvas.alpha = 0;
        _canvas.blocksRaycasts = false;
    }

}
