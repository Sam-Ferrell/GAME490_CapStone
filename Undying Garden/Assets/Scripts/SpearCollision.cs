using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{
    public static BoxCollider spearCollider;
    public static CapsuleCollider bowCollider;

    public static bool weaponBow = false;
    public static bool weaponSpear = true;

    public float attackDamage1 = 25f;
    public float attackDamage2 = 30f;
    public float attackDamage3 = 40f;

    public static bool dealDamage1 = false;
    public static bool dealDamage2 = false;
    public static bool dealDamage3 = false;

    // Start is called before the first frame update
    void Start()
    {
        spearCollider = GetComponent<BoxCollider>();
        bowCollider = GetComponent<CapsuleCollider>();

        bowCollider.enabled = !bowCollider.enabled;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void BowOut()
    {
        weaponBow = true;
        weaponSpear = false;
        spearCollider.enabled = !spearCollider.enabled;
        bowCollider.enabled = !bowCollider.enabled;
    }

    public static void SpearOut()
    {
        weaponBow = false;
        weaponSpear = true;
        spearCollider.enabled = !spearCollider.enabled;
        bowCollider.enabled = !bowCollider.enabled;
    }

    private void OnTriggerStay(Collider other)
    {
        // If the game object colliding with the position is the Alpha set arrived to true.
        if (other.tag == "Alpha" && dealDamage1 == true && weaponSpear)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage1);
        }
        if (other.tag == "Alpha" && dealDamage2 == true && weaponSpear)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage2);
        }
        if (other.tag == "Alpha" && dealDamage3 == true && weaponSpear)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage3);
        }

        if (other.tag == "Alpha" && dealDamage1 == true && weaponBow)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage1);
        }
        if (other.tag == "Alpha" && dealDamage2 == true && weaponBow)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage2);
        }
        if (other.tag == "Alpha" && dealDamage3 == true && weaponBow)
        {
            Debug.Log("You attacked the Alpha!");
            other.GetComponent<AlphaHealth>().takeDamage(attackDamage3);
        }
    }
}
