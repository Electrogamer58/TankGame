﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuff : PowerUpBase
{
    [SerializeField] AudioClip _powerDown;
    [SerializeField] Material _rage;

    [SerializeField] Renderer tankRen;
    [SerializeField] Renderer turretRen;

    //[SerializeField] GameObject core;
    [SerializeField] Renderer crystalRen;
    //[SerializeField] ParticleSystem crystalBeam;

    Material _originalMaterial;
    Material _ogCrystalMaterial;

    protected override void PowerUp(Player player)
    {
        tankRen = GameObject.Find("Tank/Art/Body").GetComponent<Renderer>();
        turretRen = GameObject.Find("Tank/Art/Turret").GetComponent<Renderer>();
        TankController _tank = tankRen.GetComponentInParent<TankController>();

        _originalMaterial = tankRen.material;

        tankRen.material = _rage;
        turretRen.material = _rage;

        _tank._recharge /= 10;
    }

    protected override void PowerDown(Player player)
    {
        tankRen.material = _originalMaterial;
        turretRen.material = _originalMaterial;
        TankController _tank = tankRen.GetComponentInParent<TankController>();

        if (_powerDown != null)
        {
            AudioHelper.PlayClip2D(_powerDown, 1f);
        }

        _tank._recharge = _tank._ogRecharge;
    }

    protected override void BossPowerUp(Boss boss)
    {
        crystalRen = GameObject.Find("Boss/Art/Core").GetComponent<Renderer>();
        //core = GameObject.Find("Boss/Art/Core");

        _ogCrystalMaterial = crystalRen.material;

        //crystalBeam = core.GetComponentInChildren<ParticleSystem>();
        //crystalBeam.startColor = new Color(1, 0, 0, 1);

        boss.attackDamage *= 3;
        boss.hasPowerup = true;
        boss.moveRoll = 1;

        crystalRen.material = _rage;


    }

    protected override void BossPowerDown(Boss boss)
    {
        //core = GameObject.Find("Boss/Art/Core");

        crystalRen.material = _ogCrystalMaterial;
    
        //crystalBeam = core.GetComponentInChildren<ParticleSystem>();
        //crystalBeam.startColor = new Color(0, 1, 1, 1);

        boss.attackDamage /= 3;
        boss.hasPowerup = false;
        

        if (_powerDown != null)
        {
            AudioHelper.PlayClip2D(_powerDown, 1f);
        }

        //Destroy(this);
    }
}
