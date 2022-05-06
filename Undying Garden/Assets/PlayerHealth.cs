using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static float health = 100f;

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
            health = 100f;
            AlphaHealth.health = 100f;
            SceneManager.LoadScene("DeathScreen");
        }
    }
}
