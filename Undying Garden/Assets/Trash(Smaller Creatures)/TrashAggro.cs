using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashAggro : StateMachineBehaviour
{
    private Transform Trash;
    private Transform player;

    public float pursueRange = 10f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Once we enter the navigate state get the transforms of the Trash and the Player.
        Trash = animator.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Distance between the Trash and the player.
        var playerDistance = Trash.position - player.position;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Navigate") && playerDistance.sqrMagnitude <= pursueRange * pursueRange)
        {
            /* Because the Trash was navigating we want to set arrived to false just in case the Trash entered the pursue state
             * while standing on a target. That way the next time we enter the navigate state arrived is guarenteed to be false.
             */
            TrashNavMesh.arrived = false;

            // Set the Pursue trigger for the state machine to traverse into the pursue state.
            animator.SetTrigger("Pursue");
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && playerDistance.sqrMagnitude <= pursueRange * pursueRange)
        {
            /* Because the Trash was navigating we want to set arrived to false just in case the Trash entered the pursue state
             * while standing on a target. That way the next time we enter the navigate state arrived is guarenteed to be false.
             */
            TrashNavMesh.arrived = false;

            // Set the Pursue trigger for the state machine to traverse into the pursue state.
            animator.SetTrigger("Pursue");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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

