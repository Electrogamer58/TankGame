using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : ProjectileBase
{
    [SerializeField] public Boss _boss = null;

    Vector3 _target;
    Vector3 _newPos;

    protected override void Awake()
    {
        base.Awake();


        DelayHelper.DelayAction(this, FindBoss, 0.5f);
         
    }
    
    private void Update()
    {
        if (_boss != null)
        {
            transform.LookAt(_boss.transform);
            _newPos = Vector3.MoveTowards(_rb.position, _target, moveSpeed);
            _rb.MovePosition(_newPos);
            
        }
        
    }

    private void FindBoss()
    {
        _boss = FindObjectOfType<Boss>().GetComponent<Boss>();
        _target = new Vector3(_boss.transform.position.x, 0.38f, _boss.transform.position.z);
    }

    protected override void DeactivateObject()
    {
        base.DeactivateObject();
        _boss = null;
    }
}
