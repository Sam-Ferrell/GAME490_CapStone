using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If the game object colliding with the position is the Trash set arrived to true.
        if (other.tag == "Trash")
        {
            Debug.Log("Arrived!");
            TrashNavMesh.arrived = true;
        }
    }
}
