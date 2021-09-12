using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBase : MonoBehaviour
{
    [SerializeField] GameObject _impactParticles;
    [SerializeField] AudioClip _impactSound;
    [SerializeField] Player _player;
    public float moveSpeed = 10;
    public float bulletDamage;

    [SerializeField] public Rigidbody _rb;

    
    Vector3 moveDirection;

    protected void Awake()
    {
        _player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();

    }

    protected void Start()
    {
        // apply vector to the rigidbody
        _rb.useGravity = false;
        //Destroy(gameObject, 10f); //destroy after 3 seconds

        DelayHelper.DelayAction(this, DeactivateObject, 10f);
    }

    //protected void FixedUpdate()
    //{
        //Vector3 moveOffset = transform.forward * moveSpeed;
        //_rb.MovePosition(_rb.position + moveOffset);
    //

    private void OnCollisionEnter(Collision collision)
    {
        //play impact
        ImpactFeedback();
        Enemy _enemy = collision.collider.GetComponent<Enemy>();
        if (_enemy != null)
        {
            _enemy.DecreaseEnemyHealth(bulletDamage);
            gameObject.SetActive(false);
        }
        
    }

    protected void ImpactFeedback()
    {
        //particles
        if (_impactParticles != null)
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

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
