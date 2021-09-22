using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHUD : MonoBehaviour
{
    public Health _health { get; private set; }

    private const float ANIMATED_BAR_WIDTH = 500f;

    [SerializeField] Slider _healthBar = null;
    [SerializeField] GameObject _redPanel = null;
    [SerializeField] CameraShake _camera = null;

    public Transform damagedBarTemplate;

    private void Awake()
    {   
        if (damagedBarTemplate == null)
        {
            damagedBarTemplate = GameObject.Find("damagedBarTemplate").transform;
        }

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
        
        //animated bar
        float beforeAnimatedBarFillAmount = _healthBar.value;
        Debug.Log("should fill at " + beforeAnimatedBarFillAmount);
        //on damage, display new health
        _healthBar.value = _health._currentHealth;
        //continue animating
        Transform damagedBar = Instantiate(damagedBarTemplate, transform);
        damagedBar.gameObject.SetActive(true);
        damagedBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(_healthBar.value * ANIMATED_BAR_WIDTH, damagedBar.GetComponent<RectTransform>().anchoredPosition.y);
        Image damageBarImage = damagedBar.GetComponent<Image>();
        //Debug.Log(damageBarImage);
        damageBarImage.fillAmount = beforeAnimatedBarFillAmount - _healthBar.value;
        


        //show damage vignette
        _redPanel.SetActive(true);
        DelayHelper.DelayAction(this, DisableRedScreen, 0.2f); //show for .2 seconds and disable

        //set camera shake speed
        _camera.shakeDuration = 3f / _health._currentHealth;
        _camera.shakeAmount = 2f / _health._currentHealth;

    }

    private void DisableRedScreen()
    {
        _redPanel.SetActive(false);
    }
    
}
