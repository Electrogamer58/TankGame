using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    //[SerializeField] int _maxHealth = 3;
    //int _currentHealth;
    public event Action Damaged = delegate { };

    [SerializeField] GameController _gc;
    private int _currentTreasure;
    public int CurrentTreasure => _currentTreasure;

    public bool isInvincible = false;

    TankController _tankController;

    private Health _myHealth;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        _myHealth = GetComponent<Health>();
        _gc._playerIsAlive = true;
    }

    public void Damage()
    {
        //Invoke the event appropriately
        Damaged?.Invoke(); // null check 
    }

    private void Update()
    {
        if (_myHealth._currentHealth <= 0)
        {
            _gc._playerIsAlive = false;
        }
    }

    protected void ImpactFeedback()
    {
        Debug.Log("Got Hit!");
    }

    public void IncreaseTreasure(int amount)
    {
        _currentTreasure += amount;

        Debug.Log("Player's Treasure: " + _currentTreasure);
    }
}
