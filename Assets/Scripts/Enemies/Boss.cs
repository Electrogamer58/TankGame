using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] ViewRange _viewRange;
    [SerializeField] Transform _bossHead;
    [SerializeField] public int attackDamage = 10;

 

    protected override void Awake()
    {
        base.Awake();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        if (_viewRange.seePlayer == true)
        {

            transform.LookAt(_playerTransform);
            
        }
    }


}
