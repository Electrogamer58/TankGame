using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Gem : StateMachineBehaviour
{
    Boss _boss;
    [SerializeField] ParticleSystemRenderer _beamParticles;
    [SerializeField] Material _gemMaterial;
    [SerializeField] Material _redBeam;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
        _boss._gemHitAllowed = true;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _beamParticles = GameObject.Find("Enemies/Boss/Art/Core/LaserBeam").GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>();

        if (_boss.hasPowerup && _beamParticles != null && _redBeam != null)
        {
            _beamParticles.material = _redBeam;
            _beamParticles.trailMaterial = _redBeam;
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_beamParticles != null && _redBeam != null)
        {
            _beamParticles.material = _gemMaterial;
            _beamParticles.trailMaterial = _gemMaterial;

        }

    }

 
}
