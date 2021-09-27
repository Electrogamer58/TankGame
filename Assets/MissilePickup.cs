using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissilePickup : CollectibleBase
{
    public event Action AddedMissile = delegate { };

    protected override void Collect(Player player)
    {
        player.GetComponent<TankController>()._missilesAmt += 3;
        MissileAdded();
    }

    private void MissileAdded()
    {
        AddedMissile?.Invoke();
    }
}
