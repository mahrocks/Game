using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5.0f;
	public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);

	private int currentHealth = 100;
	private Animator playerAnimator;
	private bool playerDamaged;
	private bool playerIsDead;

	void Awake () {
		playerAnimator = GetComponent<Animator> ();
		playerDamaged = false;
		playerIsDead = false;
	}

	void Update () {
		if (playerDamaged) {
			damageImage.color = flashColour;
		} 
		else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		playerDamaged = false;
	}

	/* Kill Player */
	void Die(){
		playerIsDead = true;
		playerAnimator.SetTrigger ("Die");
	}

	/* Decrease amount of health */
	public void TakeDamage(int amount){
		playerDamaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !playerIsDead) {
			Die ();
		}
	}

	/* increase amount of health */
	public void RecoverHealth(int amount) {
		if (currentHealth <= 100 && !playerIsDead) {
			currentHealth += amount;
			if (currentHealth > 100) {
				currentHealth = 100;
			}
			healthSlider.value = currentHealth;
		}
	}

	public bool isDead (){
		return playerIsDead;
	}
}
