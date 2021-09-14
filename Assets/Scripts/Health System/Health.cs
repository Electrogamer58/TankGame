using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : IDamageableInterface
{
    public int _maxHealth;
    public int _currentHealth;

    public float _killDelay;
    public int _heal;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if(_currentHealth <= 0)
        {
            Kill(_killDelay);
        }
    }

}
