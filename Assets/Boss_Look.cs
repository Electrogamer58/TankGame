using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Look : StateMachineBehaviour
{
    [SerializeField] float _moveSpeed = 1.5f;
    ViewRange _viewRange;
    [SerializeField] Transform _upgrade;
    Rigidbody _rb;
    Boss _boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _viewRange = animator.GetComponentInChildren<ViewRange>();
        _upgrade = GameObject.FindGameObjectWithTag("Rage").transform;
        _boss = animator.GetComponent<Boss>();
        _rb = animator.GetComponent<Rigidbody>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _upgrade = GameObject.FindGameObjectWithTag("Rage").transform;
        if (_viewRange.seePlayer == true)
        {
            animator.SetBool("seesPlayer", true);

        }

        if (_boss.moveRoll == 2 && _viewRange.seePlayer == false && _boss.hasPowerup == false)
        {
            Debug.Log("Sees Powerup");
            //_boss.LookAtPowerup();
            if (_upgrade != null && _boss.hasPowerup == false)
            {
                Vector3 _target = new Vector3(_upgrade.position.x, _rb.position.y, _upgrade.position.z);
                Vector3 _newPos = Vector3.MoveTowards(_rb.position, _target, _moveSpeed);


                _rb.MovePosition(_newPos);
                

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
