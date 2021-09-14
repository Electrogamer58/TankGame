using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamageableInterface : MonoBehaviour
{
    [Header("Basic Info and Feedback")]
    [SerializeField] Health _health;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound = null;

    [Header("Drops")]
    public bool _dropSomething = false;
    [SerializeField] GameObject _objectToDrop;
    public int _dropAmount;

    [Header("Destroy on Death?")]
    public bool _allowDestroy = false;


    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public virtual void TakeDamage(int damage)
    {
        _health._currentHealth -= damage;
        _health._currentHealth = Mathf.Clamp(_health._currentHealth, 0, _health._maxHealth);
    }

    //public virtual void Heal(int health)
    //{
        //_health._currentHealth += health;

    //}

    protected void DeathFeedback(AudioClip _feedback)
    {
        //particles
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
            
        }
        // audio. TODO: Consider object pooling - helps performance
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_feedback, 1f);
        }

        transform.position = new Vector3(transform.position.x, -100, transform.position.z);
    }

    public void Kill(float delay)
    {
        DeathFeedback(_impactSound);
        

        if (_dropSomething)
        {
            while (_dropAmount > 0)
            {
                
                Vector3 _v3 = gameObject.transform.position + new Vector3(Random.Range(-2, 2), 101, Random.Range(-2, 2));
                _objectToDrop = Instantiate(_objectToDrop, _v3, Quaternion.identity);
                _dropAmount -= 1;

            }
        }

        if (_allowDestroy)
        {
            Destroy(gameObject, delay);
        }
        else if (!_allowDestroy)
        {
            gameObject.SetActive(false);
        }

    }
}


