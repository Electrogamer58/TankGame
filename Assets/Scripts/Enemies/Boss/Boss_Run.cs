using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    [SerializeField] float _moveSpeed = 2.5f;
    [SerializeField] float _attackRange = 5f;
    [SerializeField] float _gemAttackRange = 10f;
    [SerializeField] float _bombRange = 15f;

    [SerializeField] Transform _player = null;

    
    Transform _upgrade;
    ViewRange _viewRange;
    Rigidbody _rb;
    Boss _boss;

    private int _throwOrGem = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("bossSpeed", _moveSpeed);
        

        if (GameController._playerIsAlive)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        _upgrade = GameObject.FindGameObjectWithTag("Rage").transform;
        _rb = animator.GetComponent<Rigidbody>();
        _boss = animator.GetComponent<Boss>();
        _viewRange = animator.GetComponentInChildren<ViewRange>();

        if (_viewRange.seePlayer == true)
        {

            animator.SetBool("seesPlayer", true);

        }

        if (_viewRange.seePlayer == false)
        {
            animator.SetBool("seesPlayer", false);
        }

        _throwOrGem = Random.Range(1, 3);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_boss.moveRoll == 1 || _boss.hasPowerup == true)
        {
            _boss.LookAtPlayer();

                    if (_player != null) //has player 
                    {      
                        if (_viewRange.seePlayer == false) //if doesnt see player
                        {
                            animator.SetBool("seesPlayer", false); //dont do below
                        }

                        if (_viewRange.seePlayer == true && animator.GetBool("seesPlayer") == true) //if sees player, move towards him
                        {
                            Vector3 _target = new Vector3(_player.position.x, 0.38f, _player.position.z);
                            Vector3 _newPos = Vector3.MoveTowards(_rb.position, _target, _moveSpeed);
                            _rb.MovePosition(_newPos);
                        }
                        if (Vector3.Distance(_player.position, _rb.position) <= _attackRange) //if within range, attack
                        {
                            animator.SetTrigger("Attack");                   
                        }
                        else if (Vector3.Distance(_player.position, _rb.position) > _attackRange) //if outside of attack range... 
                        {
                            if (Vector3.Distance(_player.position, _rb.position) <= _gemAttackRange) //but within beam attack range
                            {
                                animator.SetBool("seesPlayer", false); //set to stop looking at player
                                
                                if (!animator.GetBool("isHalved")) //if health is not halved yet
                                 {
                                    
                                    animator.SetTrigger("ThrowBomb"); //throw a bomb instead

                                 } else if (animator.GetBool("isHalved"))

                                    animator.SetTrigger("Special Attack"); //try and special attack

                            }
                        }

                        else if (Vector3.Distance(_player.position, _rb.position) > _gemAttackRange) //if out of range, throw bomb
                        {
                                    animator.SetBool("seesPlayer", false);
                                    Debug.Log("Within Bomb Range");
                                    animator.SetTrigger("ThrowBomb");
                        }
                    }
        }
        else if (_boss.moveRoll == 2 && _boss.hasPowerup == false)
        {
    
            if (_upgrade != null)
            {
                Vector3 _target = new Vector3(_upgrade.position.x, 0.38f, _upgrade.position.z);
                Vector3 _newPos = Vector3.MoveTowards(_rb.position, _target, _moveSpeed);

                
                _rb.MovePosition(_newPos);

            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetFloat("bossSpeed", 0);
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Special Attack");
        animator.ResetTrigger("ThrowBomb");
    }


}
