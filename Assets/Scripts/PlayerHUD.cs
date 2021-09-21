using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHUD : MonoBehaviour
{
    public Health _health { get; private set; }
    [SerializeField] Slider _healthBar = null;
    [SerializeField] GameObject _redPanel = null;
    [SerializeField] CameraShake _camera = null;

    private void Awake()
    {
        _health = GetComponent<Health>();

        _healthBar.maxValue = _health._maxHealth;
        _healthBar.value = _health._maxHealth;
    }


    private void OnEnable()
    {
        //subscribe to event
        _health.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        //subscribe to event
        _health.Damaged -= OnDamaged;
    }

    void OnDamaged()
    {
        //on damage, display new health
        _healthBar.value = _health._currentHealth;

        //show damage vignette
        _redPanel.SetActive(true);
        DelayHelper.DelayAction(this, DisableRedScreen, 0.2f); //show for .2 seconds and disable

        //set camera shake speed
        _camera.shakeDuration = 5f / _health._currentHealth;
        _camera.shakeAmount = 3f / _health._currentHealth;

    }

    private void DisableRedScreen()
    {
        _redPanel.SetActive(false);
    }
    
}
