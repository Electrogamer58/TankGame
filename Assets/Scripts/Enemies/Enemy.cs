﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;
    [SerializeField] AudioClip _DullSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
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

    protected virtual void PlayerImpact(Player player)
    {
        player.DecreaseHealth(_damageAmount);
    }

    private void ImpactFeedback()
    {
        //particles
        if(_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
            //ParticleSystem parts = _impactParticles.GetComponent<ParticleSystem>();
            //float totalDuration = parts.duration + parts.startLifetime;
            //Destroy(_impactParticles, totalDuration);
        }
        // audio. TODO: Consider object pooling - helps performance
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }

    public void Move()
    {

    }
}
