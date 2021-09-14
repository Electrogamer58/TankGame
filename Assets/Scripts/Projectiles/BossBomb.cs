using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : ProjectileBase
{
    [SerializeField] AudioClip _dullSound;
    [SerializeField] GameObject _zombie; //zombie to spawn
    //[SerializeField] Player _player;
    [SerializeField] TankController _tank;

    protected override void Start()
    {
        _rb.useGravity = true;
        DelayHelper.DelayAction(this, DeactivateObject, 10f);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject.GetComponent<Player>();
            _tank = _player.GetComponent<TankController>();
            
            if (!_player.isInvincible) //if vincible and hit, deal damage
            {
                
                _player.DecreaseHealth(bulletDamage);
                ImpactFeedback(_impactSound);
                //_player.GetComponent<Rigidbody>().AddExplosionForce(_damageAmount, _player.transform.position, 2);
                Debug.Log("Bomb");
            }
            else if (_player.isInvincible) //else, just explode but dont deal damage
            {
                ImpactFeedback(_dullSound);
            }
        }
        //if it misses, spawn a zombie

        else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            SpawnZombie();
            ImpactFeedback(_impactSound);
            this.gameObject.SetActive(false);
        }


    }

    private void SpawnZombie()
    {
        _zombie.SetActive(true);
        _zombie = Instantiate(_zombie, gameObject.transform.position, Quaternion.identity);
        Debug.Log("Spawned a Zombie");
    }
}
