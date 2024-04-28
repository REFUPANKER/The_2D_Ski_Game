using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerHeadController : MonoBehaviour
{
    public bool hit { get; set; }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            hit = true;
        }
    }
    void LateUpdate()
    {
        if (hit)
        {
            hit = false;
        }
    }
}
