using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaHealth : MonoBehaviour
{
    public float health = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Yay you beat the Alpha!");
        }
    }
}
