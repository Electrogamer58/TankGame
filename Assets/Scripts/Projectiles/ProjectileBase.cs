using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBase : MonoBehaviour
{
    [SerializeField] GameObject _impactParticles;
    [SerializeField] protected AudioClip _impactSound;
    [SerializeField] protected Player _player;
    [SerializeField] protected TankController _tank;
    public float moveSpeed = 20;
    public int bulletDamage;

    [SerializeField] public Rigidbody _rb;

    
    Vector3 moveDirection;

    protected virtual void Awake()
    {
        _player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _tank = _player.GetComponent<TankController>();
        _rb = GetComponent<Rigidbody>();

    }

    protected virtual void Start()
    {
        // apply vector to the rigidbody
        _rb.useGravity = false;
        //Destroy(gameObject, 10f); //destroy after 3 seconds

        //DelayHelper.DelayAction(this, DeactivateObject, 10f);
        Destroy(this.gameObject, 20f);

        _rb.velocity = _tank.transform.forward * moveSpeed;
    }

    //protected void FixedUpdate()
    //{
        //Vector3 moveOffset = transform.forward * moveSpeed;
        //_rb.MovePosition(_rb.position + moveOffset);
    //

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>())
        {
            //play impact
            ImpactFeedback(_impactSound);
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(bulletDamage);
            
            DelayHelper.DelayAction(this, DeactivateObject, 0.01f);
        }
        

    }

    protected void ImpactFeedback(AudioClip _feedback)
    {
        //particles
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
            Destroy(_impactParticles, 10f);


        }
        // audio. TODO: Consider object pooling - helps performance
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_feedback, 1f);
            //Destroy(_impactSound, 10f);
        }

        transform.position = new Vector3(transform.position.x, -100, transform.position.z);
    }

    protected virtual void DeactivateObject()
    {
        gameObject.SetActive(false);
        
    }
}
