using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData
{
    public string Id;
    public string Name;
    public Color BallColor;
    public float MaxYVelocity;
    public int Price;
    public bool isBought;

    public void DeserializeCustomData(string customData)
    {
        var CustomData = JsonUtility.FromJson<BallCustomData>(customData);
        ColorUtility.TryParseHtmlString(CustomData.Color, out BallColor);
        MaxYVelocity = int.Parse(CustomData.MaxYVelocity);
    }
}

struct BallCustomData
{
    public string Color;
    public string MaxYVelocity;
}