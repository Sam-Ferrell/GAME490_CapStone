using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject[] movePositionTransforms;
    private int i;
    public static bool arrived = false;

    private void Awake()
    {
        // Get the NavMeshAgent.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Choose a random index from the movePositionTransforms array.
        i = Random.Range(0, movePositionTransforms.Length);

        // Turn on the collider of the randomly selected position.
        movePositionTransforms[i].GetComponent<SphereCollider>().enabled = true;

        // Lastly make the agent move to the selected position. 
        navMeshAgent.destination = movePositionTransforms[i].transform.position;
    }

    private void Update()
    {
        /*  If arrived is true then the agent has collided with the currently selected position.
         *   - Updating the arrived variable is done by the script Collision.cs on each of the position game objects.
         */
        if (arrived == true)
        {
            // Once the agent has arrived at the currently selected position turn off its collider and choose a new position.
            movePositionTransforms[i].GetComponent<SphereCollider>().enabled = false;
            i = Random.Range(0, movePositionTransforms.Length);

            // Turn on the selected position's collider and make the agent move to it.
            movePositionTransforms[i].GetComponent<SphereCollider>().enabled = true;
            navMeshAgent.destination = movePositionTransforms[i].transform.position;

            // Set arrived back to false for the agent to arrive at this next position.
            arrived = false;
        }

        // Make the agent move toward a single target once the spacebar is pressed.
        /* 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            navMeshAgent.destination = movePositionTransform.position;
        }
        */
    }
}
