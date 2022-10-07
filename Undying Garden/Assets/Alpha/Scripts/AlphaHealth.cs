using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaHealth : MonoBehaviour
{
    private GameObject alphaObjectAnimator;

    private Animator animator;
    private Animator alphaAnimator;

    public static float health = 500f;
    public float currentAlphaHealth = 100f;

    public HealthBar alphaHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        alphaObjectAnimator = GameObject.Find("SwampScorpian");
        alphaAnimator = alphaObjectAnimator.GetComponent<Animator>();

        currentAlphaHealth = health;
        alphaHealthBar.SetMaxAlphaHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        alphaHealthBar.SetAlphaHealth(currentAlphaHealth);

        // Make the agent do something when the space bar is pressed.
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentAlphaHealth -= currentAlphaHealth;
            health -= health;
        }

        if (health <= 0)
        {
            Invoke(nameof(win), 10.0f);
        }
    }

    public void takeDamage(float damage)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            animator.SetTrigger("Damaged");
            alphaAnimator.SetTrigger("Damaged");
        }

        health -= damage;
        currentAlphaHealth -= damage;

        Invoke(nameof(resetPersue), 0.01f);
    }

    public void resetPersue()
    {

        animator.SetTrigger("Pursue");
        alphaAnimator.SetTrigger("Pursue");
        animator.ResetTrigger("Damaged");
        alphaAnimator.ResetTrigger("Damaged");
    }

    public void win()
    {
        health = 100;
        PlayerHealth.health = 100;
        SceneManager.LoadScene("VictoryScreen");
    }
}
