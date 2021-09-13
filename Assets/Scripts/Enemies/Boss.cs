using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Detect Player")] 
    [SerializeField] Transform _playerTransform;
    [SerializeField] ViewRange _viewRange;

    [Header("Drops")]
    [SerializeField] public GameObject _drop;
    [SerializeField] int _dropAmount;

    [Header("Damage and Death")]
    public bool _gemHitAllowed = false;
    [SerializeField] public int attackDamage = 10;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;

    [Header("Movement")]
    [SerializeField] Transform _rageUpgrade;

    private float nextActionTime = 0.0f;
    public float period = 5f;

    public bool hasPowerup = false;
    public int moveRoll = 1;



    protected override void Awake()
    {
        base.Awake();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rageUpgrade = GameObject.FindGameObjectWithTag("Rage").transform;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_playerTransform != null)
        {
            LookAtPlayer();
        }

        

    }

    private void Update()
    {
        
        DieIfDead();
        if (Time.time > nextActionTime)
        {
            DelayHelper.DelayAction(this, RollForMovement, 1);
            nextActionTime += period;
            // execute block of code here
        }

        Vector3 _target = new Vector3(_rb.position.x, 0.36f, _rb.position.z);
        Vector3 _newPos = Vector3.MoveTowards(_rb.position, _target, 100f);

        _rb.MovePosition(_newPos);
    }

    public void LookAtPlayer()
    {
        if (_viewRange.seePlayer == true)
        {

            transform.LookAt(_playerTransform);
            
        }
    }

    public void LookAtPowerup()
    {
        transform.LookAt(_rageUpgrade);
    }

    private void DieIfDead()
    {
        if (_health <= 0)
        {
            while(_dropAmount > 0)
            {
                Transform _tf = gameObject.transform;
                Vector3 _v3 = _tf.position + new Vector3(Random.Range(-2,2), 0, Random.Range(-2,2));
                _drop = Instantiate(_drop, _v3, Quaternion.identity);
                _dropAmount -= 1;

                if (_dropAmount == 0)
                {
                    ExplosionFeedback();
                    gameObject.SetActive(false);
                }
            }  
        }      
    }

    protected void ExplosionFeedback()
    {
        
        if (_deathParticles != null)
        {
            _deathParticles = Instantiate(_deathParticles, transform.position, Quaternion.identity);
            
        }
        
        if (_deathSound != null)
        {
            AudioHelper.PlayClip2D(_deathSound, 1f);
        }
    }

    public void RollForMovement()
    {
        moveRoll = Random.Range(1, 3);
    }
}
