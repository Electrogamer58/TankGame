using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Player _player;

    private void OnEnable()
    {
        //subscribe to event
        _player.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        //subscribe to event
        _player.Damaged -= OnDamaged;
    }

    void OnDamaged()
    {

    }
}
