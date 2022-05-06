using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaPursue : StateMachineBehaviour
{
    private Transform alpha;
    private Transform player;

    // The attack range of the Alpha.
    public float attackRange = 3f;

    public float AttackTimeout = 5f;

    private float _attackTimeoutDelta = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Once we enter the pursue state get the transforms of the Alpha and the Player.
        alpha = animator.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        var playerDistance = alpha.position - player.position;

        // If the player is within the attackRange then attack them.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pursue") && playerDistance.sqrMagnitude <= attackRange * attackRange)
        {
            if (_attackTimeoutDelta <= 0)
            {
                // Set the Attack trigger for the state machine to traverse into the attack state.
                animator.SetTrigger("Attack");
                _attackTimeoutDelta = AttackTimeout;
            }

            else
            {
                _attackTimeoutDelta -= Time.deltaTime;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* Reset the Attack trigger once we exit the pursue state so we will re-enter the pursue state from the attack state
         * automatically.
         */
        animator.ResetTrigger("Attack");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
