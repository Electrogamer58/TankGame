using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;
 

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_cameraTransform);
    }
}
