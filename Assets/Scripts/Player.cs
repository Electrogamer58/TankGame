using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    [SerializeField] int _maxHealth = 3;
    int _currentHealth;
    private int _currentTreasure;
    public int CurrentTreasure => _currentTreasure;

    public bool isInvincible = false;

    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
            _currentHealth -= amount;
            Debug.Log("Player's health: " + _currentHealth);
            if (_currentHealth <= 0)
            {
                Kill();
            } 
    }

    public void Kill()
    {
            gameObject.SetActive(false);
            //TODO
            //Play Particles
            //Play sounds
    }

    public void IncreaseTreasure(int amount)
    {
        _currentTreasure += amount;

        Debug.Log("Player's Treasure: " + _currentTreasure);
    }
}
