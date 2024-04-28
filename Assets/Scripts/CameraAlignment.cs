using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAlignment : MonoBehaviour
{
    public Transform target;
    public float smoothDelay = 0.2f;
    public float CenterDistance = 0f;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + CenterDistance, target.position.y, transform.position.z), smoothDelay);
    }
}
