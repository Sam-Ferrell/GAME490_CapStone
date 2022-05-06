using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{
    public float attackDamage = 25f;

    public static bool dealDamage = false;

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
        // If the game object colliding with the position is the Alpha set arrived to true.
        if (other.tag == "Alpha" && dealDamage == true)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage);
        }
    }
}
