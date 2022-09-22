using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaHealth : MonoBehaviour
{
    public static float health = 100f;
    public float currentAlphaHealth = 100f;

    public HealthBar alphaHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentAlphaHealth = health;
        alphaHealthBar.SetMaxAlphaHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Make the agent do something when the space bar is pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health = health - 50f;
        }
        */
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        currentAlphaHealth -= damage;

        alphaHealthBar.SetAlphaHealth(currentAlphaHealth);

        if (health <= 0)
        {
            health = 100;
            PlayerHealth.health = 100;
            SceneManager.LoadScene("VictoryScreen");
        }
    }
}
