using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaScript : MonoBehaviour
{
    [Header("map points")]
    public Transform start;
    public Transform end;

    [Header("player")]
    public Transform player;
    public Rigidbody2D velocity;

    [Header("adjustments")]
    public float layerDistance;

    public float[] defaultX = new float[3];
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            defaultX[i] = transform.GetChild(i).localPosition.x;
        }
    }
    public void Reset()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector2(defaultX[i], 0);
        }
    }

    void Update()
    {
        foreach (Transform item in this.transform)
        {
            item.Translate(-(velocity.velocity.x * Time.deltaTime / layerDistance), 0, 0);
        }
    }
}
