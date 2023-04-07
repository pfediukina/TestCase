using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Action<float> OnPlayerInArea;
    
    [SerializeField] private float time = 2f;
    
    private Rigidbody2D rb;
    private bool slow = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    public void StartGame()
    {
        rb.simulated = true;
        rb.velocity = (Vector2.up + Vector2.left) * 5f;
    }

    private void Update()
    {
        if (slow && rb != null && rb.simulated)
        {
            var v = Vector2.up * (rb.velocity.y / time);
            //Debug.Log(v);
            rb.velocity -= v;
            if (rb.velocity.y <= 0)
                slow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(rb.simulated)
            slow = true;
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if(rb.simulated && rb.velocity.y > 0)
            OnPlayerInArea.Invoke(rb.velocity.y * time);
    }
}
