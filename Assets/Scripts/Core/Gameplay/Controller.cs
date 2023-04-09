using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    [SerializeField] private Vector2 offsetY;

    [SerializeField] private Transform _followed;
    //[SerializeField] private Input
    
    private Rigidbody2D rb;
    private Vector2 nextPos;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 pos)
    {
        var worldPos = Camera.main.ScreenToWorldPoint(pos);
        nextPos = new Vector2(transform.localPosition.x, worldPos.y);
        nextPos.y = Mathf.Clamp(nextPos.y, offsetY.x + _followed.position.y, offsetY.y + _followed.transform.position.y);
        rb.MovePosition(nextPos);
        //transform.position = newPos;
    }

    public void ResetParent(bool reset)
    {
        if (reset)
        {
            transform.parent = null;
        }
        else
        {
            transform.parent = _followed;
        }
    }

    // public void MakeOffset(Vector3 pos)
    // {
    //     controllOffset = pos.y - transform.position.y;
    //     Debug.Log(controllOffset);
    // }
}
