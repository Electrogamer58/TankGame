using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIfHit : MonoBehaviour
{
    [SerializeField] Transform respawn;
    [SerializeField] Transform player;
    [SerializeField] Transform _enemy;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player = other.GetComponent<Transform>();
            player.transform.position = respawn.position;
            Physics.SyncTransforms();
        }

        if (other.tag.Equals("Boss"))
        {
            _enemy = other.GetComponent<Transform>();
            _enemy.transform.position = respawn.position;
            //Physics.SyncTransforms();
        }
    }
}
