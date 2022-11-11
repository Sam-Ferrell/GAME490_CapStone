using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash2Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If the game object colliding with the position is the Trash2 set arrived to true.
        if (other.tag == "Trash2")
        {
            Debug.Log("Arrived!");
            Trash2NavMesh.arrived = true;
        }
    }
}
