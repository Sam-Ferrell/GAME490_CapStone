using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static float health = 100f;
    public float currentHealth = 100f;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
        void Update()
        {

        }

    public void takeDamage(float damage)
    {
        health -= damage;
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (health <= 0)
        {
            health = 100;
            AlphaHealth.health = 100;
            SceneManager.LoadScene("DeathScreen");
        }
    }
}
