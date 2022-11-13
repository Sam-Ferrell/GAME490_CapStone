using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject trap;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Alpha")
        {
            AlphaNavMesh.trapped();
            Invoke(nameof(selfDestruct), 6f);
        }

        if(other.tag == "Trash")
        {
            TrashNavMesh.trapped();
            Invoke(nameof(selfDestruct), 6f);
        }
    }

    private void selfDestruct()
    {
        Destroy(trap);
    }
}
