using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        // If the game object colliding with the position is the Alpha set arrived to true.
        if (other.tag == "Alpha")
        {
            //Debug.Log("Arrived!");
            AlphaNavMesh.arrived = true;
        }
    }
}
