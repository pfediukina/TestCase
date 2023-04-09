using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour //, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private PlayerActions _actions;
    private Dictionary<InputAction, Controller> activeControls = new Dictionary<InputAction, Controller>();

    private void Awake()
    {
        _actions = new PlayerActions();
        _actions.Enable();
        _actions.Touch.Touch1.started += (ctx) => OnPointerDown(_actions.Touch.Touch1Pos, _actions.Touch.Touch1Pos.ReadValue<Vector2>());
        _actions.Touch.Touch2.started += (ctx) => OnPointerDown(_actions.Touch.Touch2Pos, _actions.Touch.Touch2Pos.ReadValue<Vector2>());
        
        _actions.Touch.Touch1.canceled += (ctx) => UnassignTouch(_actions.Touch.Touch1Pos);
        _actions.Touch.Touch2.canceled += (ctx) => UnassignTouch(_actions.Touch.Touch2Pos);
    }

    private void FixedUpdate()
    {
        foreach (var pair in activeControls)
        {
            UpdateControl(pair.Key, pair.Value);
        }
    }

    private void UpdateControl(InputAction action, Controller controller)
    {
        controller.Move(action.ReadValue<Vector2>());
    }

    private void AssignTouch(InputAction touch, Controller controller)
    {
        controller.ResetParent(true);
        activeControls.Add(touch, controller);
    }

    private void UnassignTouch(InputAction touch)
    {
        if (activeControls.ContainsKey(touch))
        {
            activeControls[touch].ResetParent(false);
            activeControls.Remove(touch);
        }
    }
    
    public void OnPointerDown(InputAction action, Vector2 screenPosition)
    {
        var pos = Camera.main.ScreenToWorldPoint(screenPosition);
        pos.z = 0;
        foreach(var col in Physics2D.OverlapCircleAll(pos, 0.25f))
        {
            if (col.TryGetComponent<Controller>(out var controller))
            {
                //controller.Move(pos);
                AssignTouch(action, controller);
            }
        }
    }
}
