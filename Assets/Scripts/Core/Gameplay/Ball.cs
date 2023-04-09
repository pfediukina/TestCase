using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Action<float> OnPlayerInArea;
    private BallData _data;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        InitBall();
    }

    private void InitBall()
    {
        _data = Player.Instance.CurrentBallData;
        GetComponent<SpriteRenderer>().color = _data.BallColor;
    }

    private void FixedUpdate()
    {
        if (rb != null && rb.velocity.y > _data.MaxYVelocity)
            rb.velocity -= Vector2.up;
    }

    public void StartGame()
    {
        rb.simulated = true;
        rb.velocity = (Vector2.up + Vector2.left) * 2f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.Instance.PlaySound(SoundTag.POP_SOUND);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("up_area"))
        {
            if (rb.simulated && rb.velocity.y > 0)
                OnPlayerInArea.Invoke(transform.position.y);
        }
    }
}
