using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = .25f;
    [SerializeField] float _turnSpeed = 2f;
    [SerializeField] public float _recharge = 2f;
    [SerializeField] ParticleSystem _muzzleFlash;
    [SerializeField] AudioClip _muzzleSound;

    [Header("Bullets and Access")]
    [SerializeField] Rigidbody _regularBullet;
    [SerializeField] GameObject _missile;
    [SerializeField] GameObject _mine;

    private Transform _barrelEnd;
    private bool _shotAllowed = true;
    public float _ogRecharge;

    public float MaxSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _barrelEnd = GameObject.Find("BarrelEnd").transform;
        _ogRecharge = _recharge;
    }

    private void FixedUpdate()
    {
        MoveTank();
        TurnTank();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _shotAllowed)
        {
            _shotAllowed = false;
            DelayHelper.DelayAction(this, ShotBoolChange, _recharge);
            ShootBullet();
        }
    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _moveSpeed;
        // create a vector from amount and direction
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void ShootBullet()
    {
        _muzzleFlash.Play();
        AudioHelper.PlayClip2D(_muzzleSound, 1f);

        _regularBullet.gameObject.SetActive(true);
        _regularBullet = Instantiate(_regularBullet, _barrelEnd.position, Quaternion.identity);
        _rb.AddForce(-transform.forward * 350f);
        _regularBullet.velocity = transform.forward * _regularBullet.GetComponent<ProjectileBase>().moveSpeed;
    }

    public void ShotBoolChange()
    {
        _shotAllowed = true;
    }
}
