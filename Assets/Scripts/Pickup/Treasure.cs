using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : CollectibleBase
{
    [SerializeField] int _treasureValue = 1;

    protected override void Collect(Player player)
    {
        player.IncreaseTreasure(_treasureValue);
    }

    protected override void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0,0,MovementSpeed);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }
}
