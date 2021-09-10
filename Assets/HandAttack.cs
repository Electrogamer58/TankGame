using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        Boss boss = gameObject.GetComponentInParent<Boss>();
        _damageAmount = boss.attackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (!player.isInvincible)
            {
                PlayerImpact(player);
                ImpactFeedback();
            }
            else
            {
                if (_DullSound != null)
                {
                    AudioHelper.PlayClip2D(_DullSound, 1f);
                }
            }
        }
    }


}
