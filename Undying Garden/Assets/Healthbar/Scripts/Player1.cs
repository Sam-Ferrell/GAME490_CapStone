using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;

	public int maxAlphaHealth = 100;
	public int currentAlphaHealth;

	public HealthBar powerBar;

	// Start is called before the first frame update
	void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		currentAlphaHealth = 0;
		powerBar.SetMaxAlphaHealth(maxAlphaHealth);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(20);
		}
    }

	void TakeDamage(int damage)
	{
		currentHealth -= damage;
		currentAlphaHealth -= damage;

		healthBar.SetHealth(currentHealth);
		powerBar.SetAlphaHealth(currentAlphaHealth);
	}
}
