using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : ProjectileBase
{
    [SerializeField] public Boss _boss;

    Vector3 _target;
    Vector3 _newPos;

    protected override void Awake()
    {
        base.Awake();

        _boss = FindObjectOfType<Boss>().GetComponent<Boss>();
        _target = new Vector3(_boss.transform.position.x, 0.38f, _boss.transform.position.z);
         
    }
    
    private void Update()
    {
        _newPos = Vector3.MoveTowards(_rb.position, _target, moveSpeed);
        _rb.MovePosition(_newPos);
    }
}
