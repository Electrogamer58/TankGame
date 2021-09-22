using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CameraFollow _camFollow;
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    private Player _player;
    private Health _health;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
        _health = _player.GetComponent<Health>();

        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void FixedUpdate()
    {
        originalPos = camTransform.localPosition;
    }


    void Update()
    {
        if (_camFollow.target != null || _health._currentHealth > 0)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                camTransform.localPosition = _camFollow.smoothedPosition;
            }
        }
        
    }
}