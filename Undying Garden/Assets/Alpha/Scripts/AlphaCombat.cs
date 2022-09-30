using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public float attackDamage = 25f;

    private bool checkingAttack = false;

    //private float attackTimeout = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack1()
    {
        if (checkingAttack == false)
        {
            checkingAttack = true;
            Invoke(nameof(attack), 2.0f);
            //Debug.Log("stinger attack complete");
        }
    }

    public void attack2()
    {
        if (checkingAttack == false)
        {
            checkingAttack = true;
            Invoke(nameof(attack), 1.7f);
            //Debug.Log("claw attack complete");
        }
    }

    public void attack()
    {
        checkingAttack = false;

        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider objects in hitObjects)
        {
            if (objects.tag == "Player")
            {
                objects.GetComponent<PlayerHealth>().takeDamage(attackDamage);
                Debug.Log("Alpha hit " + objects.name);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
