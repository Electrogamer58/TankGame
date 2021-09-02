using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerup : PowerUpBase
{

    [SerializeField] AudioClip _powerDown;
    [SerializeField] Material blue;

    [SerializeField] Renderer tankRen;
    [SerializeField] Renderer turretRen;

    Material _originalMaterial;

    protected override void PowerUp(Player player)
    {
        tankRen = GameObject.Find("Tank/Art/Body").GetComponent<Renderer>();
        turretRen = GameObject.Find("Tank/Art/Turret").GetComponent<Renderer>();

        _originalMaterial = tankRen.material;

        tankRen.material = blue;
        turretRen.material = blue;

        player.isInvincible = true;
    }

    protected override void PowerDown(Player player)
    {
        tankRen.material = _originalMaterial;
        turretRen.material = _originalMaterial;

        if (_powerDown != null)
        {
            AudioHelper.PlayClip2D(_powerDown, 1f);
        }

        player.isInvincible = false;
    }
}
