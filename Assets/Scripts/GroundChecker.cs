using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public LayerMask layer;
    public float distance = 0.3f;
    public float rotaterDistance = 5f;
    public bool Grounded { get; set; }
    public RaycastHit2D hit;
    public RaycastHit2D rotater;
    public float distanceToGround
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, layer);
            if (hit.collider != null)
            {
                return hit.distance;
            }
            return Mathf.Infinity;
        }
    }
    private void SendHit()
    {
        hit = Physics2D.Raycast(transform.position, -transform.up, distance, layer);
        rotater = Physics2D.Raycast(transform.position, -transform.up, rotaterDistance, layer);
    }
    void Start() { SendHit(); }
    void Update()
    {
        SendHit();
        if (hit.collider != null)
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }
    }
}
