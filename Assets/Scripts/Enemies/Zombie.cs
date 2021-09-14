using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] public Player _player;
    [SerializeField] float moveSpeed;

    Vector3 _target;
    Vector3 _newPos;

    protected override void Awake()
    {
        base.Awake();

        _player = FindObjectOfType<Player>().GetComponent<Player>();

    }

    private void Update()
    {
        if(_player != null)
        {
            _target = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
            _newPos = Vector3.MoveTowards(_rb.position, _target, moveSpeed);
            _rb.MovePosition(_newPos);
        }
        
    }

    protected override void PlayerImpact(Player player)
    {
        base.PlayerImpact(player);
        ImpactFeedback();
        player.GetComponent<Rigidbody>().AddExplosionForce(800f, gameObject.transform.position, 10f);
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject, 30f);
    }

   
}
