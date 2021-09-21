using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : ProjectileBase
{
    [SerializeField] AudioClip _dullSound;
    [SerializeField] GameObject _zombie; //zombie to spawn
    [SerializeField] Boss _boss;
    //[SerializeField] Player _player;

    protected override void Start()
    {
        _rb.useGravity = true;
        _boss = FindObjectOfType<Boss>();
        DelayHelper.DelayAction(this, DeactivateObject, 10f);

        _rb.velocity = _boss.transform.forward * moveSpeed;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            if (collision.gameObject.tag == "Player")
            {
                _player = collision.gameObject.GetComponent<Player>();
                _tank = _player.GetComponent<TankController>();

                if (!_player.isInvincible) //if vincible and hit, deal damage
                {

                    _player.GetComponent<Health>().TakeDamage(bulletDamage);
                    ImpactFeedback(_impactSound);
                    //_player.GetComponent<Rigidbody>().AddExplosionForce(_damageAmount, _player.transform.position, 2);
                    Debug.Log("Bomb");
                }
                else if (_player.isInvincible) //else, just explode but dont deal damage
                {
                    ImpactFeedback(_dullSound);
                }
            }
        }
        //if it misses, spawn a zombie

        else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            SpawnZombie();
            ImpactFeedback(_impactSound);
            Destroy(this.gameObject, 10f);
            this.gameObject.SetActive(false);

        }


    }

    private void SpawnZombie()
    {
        //_zombie.SetActive(true);
        Instantiate(_zombie, gameObject.transform.position, Quaternion.identity);
        Debug.Log("Spawned a Zombie");
    }
}
