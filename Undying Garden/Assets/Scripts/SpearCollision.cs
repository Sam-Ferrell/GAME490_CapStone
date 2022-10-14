using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{
    public float attackDamage1 = 25f;
    public float attackDamage2 = 30f;
    public float attackDamage3 = 40f;

    public static bool dealDamage1 = false;
    public static bool dealDamage2 = false;
    public static bool dealDamage3 = false;

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
        if (other.tag == "Alpha" && dealDamage1 == true)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage1);
        }
        if (other.tag == "Alpha" && dealDamage2 == true)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage2);
        }
        if (other.tag == "Alpha" && dealDamage3 == true)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage3);
        }
    }
}
