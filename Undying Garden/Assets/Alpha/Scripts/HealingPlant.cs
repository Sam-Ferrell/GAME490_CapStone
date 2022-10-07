using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlant : MonoBehaviour
{
    public float HealthRestoreAmount = 25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().restoreHealth(HealthRestoreAmount);
            Destroy(gameObject);
        }
    }
}
