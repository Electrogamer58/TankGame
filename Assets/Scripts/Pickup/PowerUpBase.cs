using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    
    protected abstract void PowerUp(Player player);

    protected abstract void PowerDown(Player player);

    protected abstract void BossPowerUp(Boss boss);

    protected abstract void BossPowerDown(Boss boss);

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed => _movementSpeed;

    [SerializeField] int _powerupDuration = 5;
    private IEnumerator powDur;
    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    private Player player;
    private Boss boss;

    protected Rigidbody _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        // calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<Player>();
        boss = other.gameObject.GetComponent<Boss>();
        if (player != null && boss == null)
        {
            PowerUp(player);
            //spawn particles and sfx bc we need to disable object
            Feedback();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            powDur = PlayerPowerDuration(_powerupDuration);
            StartCoroutine(powDur); 
        }

        if (player == null && boss != null)
        {
            BossPowerUp(boss);
            //spawn particles and sfx bc we need to disable object
            Feedback();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            powDur = BossPowerDuration(_powerupDuration);
            StartCoroutine(powDur);
        }
    }

    private void Feedback()
    {
        // particles
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }
        // audio. TODO - consider object pooling
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }

    private IEnumerator PlayerPowerDuration(int waitTime)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(waitTime);
        PowerDown(player);
        Debug.Log("Coroutine ended");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    private IEnumerator BossPowerDuration(int waitTime)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(waitTime);
        BossPowerDown(boss);
        Debug.Log("Coroutine ended");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }



}
