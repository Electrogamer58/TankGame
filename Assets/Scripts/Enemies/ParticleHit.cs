using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    [SerializeField] int _damageAmount;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;
    [SerializeField] AudioClip _dullSound;
    Boss _boss;
    Player _player;
    TankController _tank;
    private float ogSpeed = .15f;

    private void Awake()
    {
        _boss = GetComponentInParent<Boss>();
        _damageAmount = _boss.attackDamage;
    }

    private void OnParticleCollision(GameObject other)
    { 
        if (other.tag == "Player" && _boss._gemHitAllowed)
        {
            _player = other.GetComponent<Player>();
            _tank = _player.GetComponent<TankController>();
            //ogSpeed = _tank.MaxSpeed;
            if (!_player.isInvincible)
            {
                StartCoroutine(SlowDown());
                _player.GetComponent<Health>().TakeDamage(_damageAmount);
                ImpactFeedback(_impactSound);
                //_player.GetComponent<Rigidbody>().AddExplosionForce(_damageAmount, _player.transform.position, 2);
                Debug.Log("Particle Hit Player");
            }
            else if (_player.isInvincible)
            {
                ImpactFeedback(_dullSound);
            }
            _boss._gemHitAllowed = false;    
        }
        
    }

    protected void ImpactFeedback(AudioClip sound)
    {
        //particles
        //if (_impactParticles != null)
        //{
            //_impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
            //ParticleSystem parts = _impactParticles.GetComponent<ParticleSystem>();
            //float totalDuration = parts.duration + parts.startLifetime;
            //Destroy(_impactParticles, totalDuration);
        //}
        // audio. TODO: Consider object pooling - helps performance
        if (_impactSound != null || _dullSound != null)
        {
            Debug.Log("Play Explosion Sound");
            AudioHelper.PlayClip2D(sound, 1f);
        }
    }

    IEnumerator SlowDown()
    {
        
        _tank.MaxSpeed = _tank.MaxSpeed / 2;
        Debug.Log("Speed Decreased");

        yield return new WaitForSeconds(0.5f);

        _tank.MaxSpeed = ogSpeed;
        Debug.Log("Speed Increased");
    }
}
