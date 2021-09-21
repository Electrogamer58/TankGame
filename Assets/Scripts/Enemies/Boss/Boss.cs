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
    private int _health;
    public bool _gemHitAllowed = false;
    [SerializeField] public int attackDamage = 10;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;

    [Header("Bomb Options")]
    [SerializeField] GameObject _bomb;
    [SerializeField] Rigidbody _bombRB;
    [SerializeField] Transform _bombSource;
    public bool _throwBomb;

    [Header("Movement")]
    [SerializeField] Transform _rageUpgrade;

    private float nextActionTime = 0.0f;
    public float period = 5f;

    public bool hasPowerup = false;
    public int moveRoll = 1;



    protected override void Awake()
    {
        base.Awake();
        _throwBomb = false;
        _bombRB = _bomb.GetComponent<Rigidbody>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rageUpgrade = GameObject.FindGameObjectWithTag("Rage").transform;
        _health = GetComponent<Health>()._currentHealth;
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
        
        DropIfDead();
        if (Time.time > nextActionTime)
        {
            DelayHelper.DelayAction(this, RollForMovement, 1);
            nextActionTime += period;
            // execute block of code here
        }

        //Vector3 _target = new Vector3(_rb.position.x, 0.36f, _rb.position.z);
        //Vector3 _newPos = Vector3.MoveTowards(_rb.position, _target, 100f);

        //_rb.MovePosition(_newPos);
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

    private void DropIfDead()
    {
        if (_health <= 0)
        {
            while(_dropAmount > 0)
            {
                Transform _tf = gameObject.transform;
                Vector3 _v3 = _tf.position + new Vector3(Random.Range(-2,2), 0, Random.Range(-2,2));
                _drop = Instantiate(_drop, _v3, Quaternion.identity);
                _dropAmount -= 1;

            }  
        }      
    }

    //protected void ExplosionFeedback()
    //{
        
        //if (_deathParticles != null)
        //{
            //_deathParticles = Instantiate(_deathParticles, transform.position, Quaternion.identity);
            
        //}
        
        //if (_deathSound != null)
        //{
            //AudioHelper.PlayClip2D(_deathSound, 1f);
        //}
    //}

    public void RollForMovement()
    {
        moveRoll = Random.Range(1, 3);
    }

    public void ThrowBomb()
    {
        
            LookAtPlayer();
            Debug.Log("Spawn Bomb");
            //_bombRB.gameObject.SetActive(true);
            Instantiate(_bombRB, _bombSource.position, Quaternion.identity);
            _bombRB.velocity = transform.forward * _bombRB.GetComponent<ProjectileBase>().moveSpeed;
            _rb.AddForce(-transform.forward * 500f);
            
        
    }
}
