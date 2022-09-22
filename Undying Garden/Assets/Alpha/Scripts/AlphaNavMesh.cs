using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlphaNavMesh : MonoBehaviour
{
    public static NavMeshAgent navMeshAgent;

    // movePositionTransforms is a serialized field so you can add target transforms in the inspector. 
    [SerializeField] private GameObject[] targetTransforms;
    [SerializeField] private GameObject[] fleeTargetTransforms;
    private Animator animator;
    private Transform alpha;
    private Transform player;
    private int i;
    private int prev_i;
    private int j;
    private int prev_j;
    public static bool arrived = false;
    private Vector3 playerDistance;
    public float fleeRange = 30f;

    private GameObject alphaHealthBar;

    private bool hasFled1 = false;
    private bool hasFled2 = false;

    private float currHealth1;
    private float currHealth2;

    public Transform startingPosition;


    private void Awake()
    {
        alphaHealthBar = GameObject.FindGameObjectWithTag("Alpha Health");

        // Get the NavMeshAgent.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Get the Animator.
        animator = GetComponent<Animator>();

        // 
        alpha = GetComponent<Transform>();

        alpha.transform.position = startingPosition.position;

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

        // Making health states for fleeing
        currHealth1 = AlphaHealth.health * 0.66f;
        currHealth2 = AlphaHealth.health * 0.33f;
    }

    private void Update()
    { 

        // If the Alpha is in the Navigate state then navigate the world.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Navigate"))
        {
            alphaHealthBar.SetActive(false);
            /* If arrived is true then the agent has collided with the currently selected target so we call navigate().
             *  - Updating the arrived variable is done by the script Collision.cs on each of the position game objects.
             */
            if (arrived == true)
            {
                navigate();
            }
        }

        //  If the Alpha is pursuing the player call pursue()
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pursue"))
        {
            alphaHealthBar.SetActive(true);
            pursue();
        }

        // If the Alpha is fleeing from the player...
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Flee")) 
        {
            // Calculate the distance from the agent to the player.
            playerDistance = alpha.position - player.position;

            // If the Alpha has made it further than the fleeRange away from the player or arrives at a flee point then...
            if (playerDistance.sqrMagnitude >= fleeRange * fleeRange || arrived == true)
            {
                // Teleport the agent to the flee target.
                navMeshAgent.Warp(fleeTargetTransforms[j].transform.position);

                // Set the state machine to the navigate state.
                animator.ResetTrigger("Flee");
                animator.SetTrigger("Navigate");

                // Reset the speed and angular speed of the agent.
                navMeshAgent.speed = navMeshAgent.speed / 3;
                navMeshAgent.angularSpeed = navMeshAgent.angularSpeed / 3;
            }          
        }

        /*
        // Make the agent do something when the space bar is pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flee();
        }
        */
        
    }

    // navigate() selects a new target and sets it as the agent's destination. Essentually it helps the agent to navigate.
    public void navigate()
    {
        fleeTargetTransforms[j].GetComponent<SphereCollider>().enabled = false;

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

        //Debug.Log(i);

        // Turn on the selected position's collider and make the agent move to it.
        targetTransforms[i].GetComponent<SphereCollider>().enabled = true;
        navMeshAgent.destination = targetTransforms[i].transform.position;

        // Set arrived back to false for the agent to arrive at this next target.
        arrived = false;
    }

    // pursue() makes the agent pursue the player.
    public void pursue()
    {
        /* Because we were previously in the navigate state we need to disable the collider of the target the Alpha was previously
         * navigating towards.
         */ 
        targetTransforms[i].GetComponent<SphereCollider>().enabled = false;

        // Make the Alpha target the player.
        navMeshAgent.destination = player.position;

        // Alpha will flee if health ever hits a quarter of it's max health 
        if ((AlphaHealth.health <= currHealth1) && (hasFled1 == false))
        {
            flee();
            hasFled1 = true;
        }

        if ((AlphaHealth.health <= currHealth2) && (hasFled2 == false))
        {
            flee();
            hasFled2 = true;
        }
    }

    // flee() makes the agent run away from the player to a random target.
    public void flee()
    {
        Debug.Log("flee() was called!");
        // Set the state machine to the flee state by setting the trigger Flee.
        animator.ResetTrigger("Pursue");
        animator.SetTrigger("Flee");

        // Randomly select a new target like usual. 
        while (j == prev_j)
        {
            j = Random.Range(0, fleeTargetTransforms.Length);
        }
        prev_j = j;

        // Enable the new target's collider and set it to the agent's desitination to the new target.
        fleeTargetTransforms[j].GetComponent<SphereCollider>().enabled = true;
        navMeshAgent.destination = fleeTargetTransforms[j].transform.position;

        // Increase the agent's speed and angular speed so it can get away from the player.
        navMeshAgent.speed = navMeshAgent.speed * 3;
        navMeshAgent.angularSpeed = navMeshAgent.angularSpeed * 3;
    }
}
