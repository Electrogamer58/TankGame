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

    GameController _gc;
    Transform _upgrade;
    ViewRange _viewRange;
    Rigidbody _rb;
    Boss _boss;

    private int _throwOrGem = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("bossSpeed", _moveSpeed);
        _gc = GameObject.Find("GameController").GetComponent<GameController>();

        if (_gc._playerIsAlive)
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
            //Debug.Log("Rolled a 1");
            _boss.LookAtPlayer();
                    if (_player != null)
                    {
                        

                        

                        if (_viewRange.seePlayer == false)
                        {
                            animator.SetBool("seesPlayer", false);
                        }


                        if (_viewRange.seePlayer == true && animator.GetBool("seesPlayer") == true)
                        {
                            Vector3 _target = new Vector3(_player.position.x, 0.38f, _player.position.z);
                            Vector3 _newPos = Vector3.MoveTowards(_rb.position, _target, _moveSpeed);
                            _rb.MovePosition(_newPos);

                        }

                        if (Vector3.Distance(_player.position, _rb.position) <= _attackRange)
                        {
                            animator.SetTrigger("Attack");
                        }

                        else if (Vector3.Distance(_player.position, _rb.position) > _attackRange && _throwOrGem == 1)
                        {
                            if (Vector3.Distance(_player.position, _rb.position) <= _gemAttackRange)
                            {
                                animator.SetBool("seesPlayer", false);
                                animator.SetTrigger("Special Attack");
                                //animator.SetBool("seesPlayer", true);
                            }
                        }

                        else if (Vector3.Distance(_player.position, _rb.position) > _gemAttackRange && _throwOrGem == 2)
                        {
                            
                                //if (Vector3.Distance(_player.position, _rb.position) <= _bombRange)
                                //{
                                    animator.SetBool("seesPlayer", false);
                                    Debug.Log("Within Bomb Range");
                                    animator.SetTrigger("ThrowBomb");
                                    //_boss.ThrowBomb();
                                    //animator.SetBool("seesPlayer", true);
                                //}

                            
                        }
                    }
        }
        else if (_boss.moveRoll == 2 && _boss.hasPowerup == false)
        {
            //Debug.Log("Rolled a 2");
            //_boss.LookAtPowerup();
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
