using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] ViewRange _viewRange;

    private bool allowedLook = false;

    // Update is called once per frame
    private void Awake()
    {
        DelayHelper.DelayAction(this, ChangeToTrue, 8);
    }

    void FixedUpdate()
    {
         
         WatchPlayer();
        
    }


    public void WatchPlayer()
    {
        if (_playerTransform != null && _viewRange.seePlayer == true && allowedLook)
        {

            transform.LookAt(_playerTransform);

        }
    }

    public void ChangeToTrue()
    {
        allowedLook = true;
    }

   
    public void ChangeBool(bool boolean)
    {
        allowedLook = boolean;
    }


}
