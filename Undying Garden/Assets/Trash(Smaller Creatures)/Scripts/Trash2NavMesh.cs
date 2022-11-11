using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trash2NavMesh : MonoBehaviour
{
    public static NavMeshAgent navMeshAgent;

    // movePositionTransforms is a serialized field so you can add target transforms in the inspector. 
    [SerializeField] private GameObject[] targetTransforms;
    private Animator animator;
    private Transform Trash2;
    private Transform player;
    private int i;
    private int prev_i;
    private int j;
    private int prev_j;
    public static bool arrived = false;
    private Vector3 playerDistance;
    public float pursueRange;
    public float fleeRange = 30f;

    Rigidbody Rigidbody;

    private GameObject Trash2ObjectAnimator;
    private Animator Trash2Animator;

    private bool trash2Health;

    private float feedOrIdleLength;

    private bool stopMoving = true;
    private bool dead = true;

    public Transform startingPosition;

    private void Awake()
    {

        // Get the NavMeshAgent.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Get the Animator.
        animator = GetComponent<Animator>();

        // 
        Trash2 = GetComponent<Transform>();

        Rigidbody = GetComponent<Rigidbody>();

        Trash2ObjectAnimator = GameObject.Find("Trash2");
        Trash2Animator = Trash2ObjectAnimator.GetComponent<Animator>();

        trash2Health = GetComponent<Trash2Health>();

        Trash2.transform.position = startingPosition.position;

        // Get the transform of the Player.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Choose a random index from the movePositionTransforms array.
        i = Random.Range(0, targetTransforms.Length);
        //Debug.Log(i);

        j = 0;
        prev_j = 0;

        /* Save the "previous" i selected so we can make sure the next time we choose a new i it is not the same one we chose
         * previously.
         */
        prev_i = i;

        // Turn on the collider of the randomly selected position.
        targetTransforms[i].GetComponent<SphereCollider>().enabled = true;

        // Lastly make the agent move to the selected position. 
        navMeshAgent.destination = targetTransforms[i].transform.position;
    }

    private void Update()
    {

        // If the Trash2 is in the Navigate state then navigate the world.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Navigate") && dead == true)
        {
            /* If arrived is true then the agent has collided with the currently selected target so we call navigate().
             *  - Updating the arrived variable is done by the script Collision.cs on each of the position game objects.
             */
            if (arrived == true && stopMoving == true)
            {
                stopMoving = false;
                Debug.Log("Idle has Begun");
                animator.ResetTrigger("Navigate");
                animator.SetTrigger("Idle");
                Trash2Animator.ResetTrigger("Navigate");
                Trash2Animator.SetTrigger("Idle");

                Invoke(nameof(IdleFeedRando), 3.0f);
                //Invoke(nameof(GetGoing), 3.0f);
            }
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Aggro") && dead == true)
        {
            //Debug.Log("Aggro");
            animator.ResetTrigger("Navigate");
            Trash2Animator.ResetTrigger("Navigate");
            navMeshAgent.speed = 0.1f;
        }

        //  If the Trash2 is pursuing the player call pursue()
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pursue") && dead == true)
        {
            animator.ResetTrigger("Navigate");
            Trash2Animator.ResetTrigger("Navigate");
            animator.ResetTrigger("Aggro");
            Trash2Animator.ResetTrigger("Aggro");

            playerDistance = Trash2.position - player.position;
            //navMeshAgent.stoppingDistance = 5;

            if (playerDistance.sqrMagnitude <= pursueRange)
            {
                navMeshAgent.speed = 0.1f;
            }

            else if (playerDistance.sqrMagnitude >= pursueRange)
            {
                navMeshAgent.speed = 3f;
            }

            pursue();
        }

        if (Trash2Health.health <= 0 && dead == true)
        {
            Debug.Log("Trash2 Dead");

            dead = false;

            navMeshAgent.speed = 0;
            navMeshAgent.angularSpeed = 0;

            Rigidbody.constraints = RigidbodyConstraints.FreezePosition;

            animator.ResetTrigger("Pursue");
            Trash2Animator.ResetTrigger("Pursue");
            animator.SetTrigger("Death");
            Trash2Animator.SetTrigger("Death");
        }

    }

    public void IdleFeedRando()
    {
        feedOrIdleLength = Random.Range(3f, 10f);

        Debug.Log("Idle Longer for " + feedOrIdleLength);
        Invoke(nameof(GetGoing), feedOrIdleLength);

    }

    public void GetGoing()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pursue"))
        {
            Debug.Log("Works");
            animator.SetTrigger("Navigate");
            animator.ResetTrigger("Idle");
            Trash2Animator.SetTrigger("Navigate");
            Trash2Animator.ResetTrigger("Idle");
            navigate();
        }
    }

    // navigate() selects a new target and sets it as the agent's destination. Essentually it helps the agent to navigate.
    public void navigate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pursue"))
        {
            animator.SetBool("Return", true);
            Trash2Animator.SetBool("Return", true);
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pursue"))
        {
            animator.SetBool("Return", false);
            Trash2Animator.SetBool("Return", false);
        }

        animator.SetTrigger("Navigate");
        Trash2Animator.SetTrigger("Navigate");
        animator.ResetTrigger("Pursue");
        Trash2Animator.ResetTrigger("Pursue");

        stopMoving = true;

        // Once the agent has arrived at the currently selected position turn off its collider and choose a new target.
        targetTransforms[i].GetComponent<SphereCollider>().enabled = false;

        /* Choose a new target and make sure we don't choose the previous target (if we did the agent wouldn't move). To do so
         * we will continuously select a new index i until it does not equal the index we previously chose.
         */
        while (i == prev_i)
        {
            i = Random.Range(0, targetTransforms.Length);
        }

        // Make sure assign the newly selected index to the previous index for the next time we need to select a new one.
        prev_i = i;

        Debug.Log(i);

        // make the agent move to new position.
        navMeshAgent.destination = targetTransforms[i].transform.position;

        // Set arrived back to false for the agent to arrive at this next target.
        arrived = false;

        // Turn on the selected position's colliders after certain time to prevent agent from getting stuck
        Invoke(nameof(engageNavCollider), 2.0f);
    }

    public void engageNavCollider()
    {
        targetTransforms[i].GetComponent<SphereCollider>().enabled = true;
    }

    // pursue() makes the agent pursue the player.
    public void pursue()
    {
        stopMoving = true;

        navMeshAgent.speed = navMeshAgent.speed * 2;

        /* Because we were previously in the navigate state we need to disable the collider of the target the Trash2 was previously
         * navigating towards.
         */
        targetTransforms[i].GetComponent<SphereCollider>().enabled = false;

        // Make the Trash2 target the player.
        navMeshAgent.destination = player.position;

        // Trash2 will return if player runs out of range 
        /*if (allowedToReturn == true)
        {
            Debug.Log("Returning");
            animator.SetTrigger("Navigate");
            animator.ResetTrigger("Pursue");
            Trash2Animator.SetTrigger("Navigate");
            Trash2Animator.ResetTrigger("Pursue");
            navigate();
        }*/

        // Calculate the distance from the agent to the player.
        playerDistance = Trash2.position - player.position;

        // If the Alpha has made it further than the fleeRange away from the player or arrives at a flee point then...
        if (playerDistance.sqrMagnitude >= fleeRange * fleeRange || arrived == true)
        {
            Debug.Log("Returning");

            animator.SetBool("Return", true);
            Trash2Animator.SetBool("Return", true);

            navigate();
            Invoke(nameof(Trash2Retreat), 1f);
        }

    }

    public void Trash2Retreat()
    {
        animator.SetBool("Return", false);
        Trash2Animator.SetBool("Return", false);
    }
}
