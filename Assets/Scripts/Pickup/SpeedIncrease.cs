using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class SpeedIncrease : CollectibleBase
{
    public event Action SpedUp = delegate { };

    [SerializeField] float _speedAmount = 0.5f;

    protected override void Collect(Player player)
    {
        //pull motor controller from player
        TankController controller = player.GetComponent<TankController>();
        if(controller != null)
        {
            controller.MaxSpeed += _speedAmount;
            SpeedUp();
        }
    }

    protected override void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(MovementSpeed, MovementSpeed, MovementSpeed);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void SpeedUp()
    {
        //Invoke the event appropriately
        SpedUp?.Invoke(); // null check 
    }

}
