using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    protected override void PlayerImpact(Player player)
    {
        TankController tankController = player.GetComponentInParent<TankController>();
        if (tankController != null)
        {
            tankController.MaxSpeed = tankController.MaxSpeed / 2;
            Debug.Log("Speed Reduced to " + tankController.MaxSpeed);
        }
    }
}
