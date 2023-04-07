using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField] private float startY;
    private float newY;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
        Debug.Log("Start " + startY);
    }

    private void Start()
    {
        newY = startY;
    }

    private void Update()
    {
        rb.MovePosition(new Vector2(transform.position.x, newY));
    }


    public void ValueChanged(Slider slider)
    {
        newY = slider.value * speed + startY;
        //Debug.Log(newY);
    }
}