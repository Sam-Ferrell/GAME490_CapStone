using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	private Animator animator;
	private GameObject alpha;

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(float health)
	{
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	public void SetMaxAlphaHealth(float alphaHealth)
	{
		slider.maxValue = alphaHealth;
		slider.value = alphaHealth;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetAlphaHealth(float alphaHealth)
	{
		slider.value = alphaHealth;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	private void Awake()
	{

	}

	void Update()
	{

	}

}
