using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _damageAmount = 1;
    [SerializeField] protected float _health = 10;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;
    [SerializeField] protected AudioClip _DullSound;

    Rigidbody _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
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

    protected void ImpactFeedback()
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

    public void DecreaseEnemyHealth(float amount)
    {
        _health -= amount;
    }

    public void Move()
    {

    }
}
