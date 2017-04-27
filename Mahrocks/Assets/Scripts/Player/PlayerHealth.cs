using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

	public int startingHealth = 100;
	public Slider healthSlider;
	public Image damageImage;
	public RawImage deathImage;
	public float flashSpeed = 5.0f;
	public float deathMessageSpeed = 50.0f;
	public Color flashColour = new Color (1.0f, 0.0f, 0.0f, 0.1f);
	public Color deathColour = new Color (1.0f, 1.0f, 1.0f, 1.0f);

	private int currentHealth;
	private Animator playerAnimator;
	private bool playerDamaged;
	private bool playerIsDead;

	AudioSource playerAudio;
	// Reference to the AudioSource component.
	public AudioClip deathClip;
	// The audio clip to play when the player dies.

	void Awake ()
	{
		playerAnimator = GetComponent<Animator> ();
		playerDamaged = false;
		playerIsDead = false;
		playerAudio = GetComponent <AudioSource> ();
		currentHealth = startingHealth;
	}

	void Update ()
	{
		if (playerDamaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		if (playerIsDead) {
			deathImage.color = Color.Lerp (deathImage.color, deathColour, deathMessageSpeed * Time.deltaTime);
		} else {
			deathImage.color = Color.clear;
		}
			
		playerDamaged = false;
	}

	/* Kill Player */
	void Die ()
	{
		playerAudio.clip = deathClip;
		playerAudio.Play ();
		playerIsDead = true;
		playerAnimator.SetTrigger ("Die");
	}

	/* Decrease amount of health */
	public void TakeDamage (int amount)
	{
		playerDamaged = true;
		playerAudio.Play ();
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !playerIsDead) {
			Die ();
		}
	}

	/* increase amount of health */
	public void RecoverHealth (int amount)
	{
		if (currentHealth <= 100 && !playerIsDead) {
			currentHealth += amount;
			if (currentHealth > 100) {
				currentHealth = 100;
			}
			healthSlider.value = currentHealth;
		}
	}

	public bool isDead ()
	{
		return playerIsDead;
	}
}
