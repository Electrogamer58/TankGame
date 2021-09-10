using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    [SerializeField] float _moveSpeed = 2.5f;
    [SerializeField] float _attackRange = 5f;
    Transform _player;
    ViewRange _viewRange;
    Rigidbody _rb;
    Boss _boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("bossSpeed", _moveSpeed);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = animator.GetComponent<Rigidbody>();
        _boss = animator.GetComponent<Boss>();
        _viewRange = animator.GetComponentInChildren<ViewRange>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss.LookAtPlayer();

        Vector3 _target = new Vector3(_player.position.x, _rb.position.y, _player.position.z);
        Vector3 _newPos = Vector3.MoveTowards(_rb.position, _target, _moveSpeed * Time.fixedDeltaTime);

        if (_viewRange.seePlayer == true)
        {
            _rb.MovePosition(_newPos);
        }

        if (_viewRange.seePlayer == false)
        {
            animator.SetBool("seesPlayer", false);
        }

        if (Vector3.Distance(_player.position, _rb.position) <= _attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetFloat("bossSpeed", 0);
        animator.ResetTrigger("Attack");
    }


}
