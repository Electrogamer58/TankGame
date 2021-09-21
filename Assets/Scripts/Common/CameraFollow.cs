using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 desiredPosition;
    public Vector3 smoothedPosition;

    void FixedUpdate()
    {
        if (target != null)
        {
            desiredPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

        //transform.LookAt(target);
    }

}