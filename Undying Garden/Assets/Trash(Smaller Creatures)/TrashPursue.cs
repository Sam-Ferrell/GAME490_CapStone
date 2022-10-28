using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPursue : StateMachineBehaviour
{
    private Transform Trash;
    private Transform player;

    private int animation;

    public TrashCombat checkAttack;

    private bool stopAttacking = false;

    // The attack range of the Trash.
    public float attackRange = 3f;

    public float AttackTimeout = 5f;

    private float _attackTimeoutDelta = 0f;

    public float returnLength = 30f;

    private bool canReturn = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Once we enter the pursue state get the transforms of the Trash and the Player.
        Trash = animator.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        checkAttack = GameObject.Find("Trash").GetComponent<TrashCombat>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerDistance = Trash.position - player.position;

        // If the player is within the attackRange then attack them.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pursue") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged") && playerDistance.sqrMagnitude <= attackRange * attackRange)
        {
            if (_attackTimeoutDelta <= 0 && stopAttacking == false)
            {
                stopAttacking = true;

                animation = Random.Range(0, 3);
                if (animation == 0)
                {
                    // Set the Attack trigger for the state machine to traverse into the attack state.
                    animator.SetTrigger("Attack1");
                    _attackTimeoutDelta = AttackTimeout;
                    checkAttack.attack1();
                    stopAttacking = false;
                    //Debug.Log("stinger attack");
                }
                else
                {
                    // Set the Attack trigger for the state machine to traverse into the attack state.
                    animator.SetTrigger("Attack2");
                    _attackTimeoutDelta = AttackTimeout;
                    checkAttack.attack2();
                    stopAttacking = false;
                    //Debug.Log("claw attack");
                }
            }

            else
            {
                _attackTimeoutDelta -= Time.deltaTime;
            }


        }

        else if (playerDistance.sqrMagnitude >= returnLength * returnLength)
        {
            Debug.Log("Returning");
            animator.SetBool("Return", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* Reset the Attack trigger once we exit the pursue state so we will re-enter the pursue state from the attack state
         * automatically.
         */
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
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
