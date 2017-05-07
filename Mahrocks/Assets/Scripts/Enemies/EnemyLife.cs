using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
	public int startingHealth = 3;
	// The amount of health the enemy starts the game with.
	public int currentHealth;
	// The current health the enemy has.
	public int scoreValue = 10;
	// The amount added to the player's score when the enemy dies.
	public AudioClip deathClip;
	// The sound to play when the enemy dies.


	public ParticleSystem blood;






	AudioSource enemyAudio;
	// Reference to the audio source.
	BoxCollider boxCollider;
	// Reference to the capsule collider.

	public bool isDead;
	public bool isFading;

	void Start(){
		currentHealth = startingHealth;
		isDead = false;
		isFading = false;
	}

	void OnCollisionEnter(Collision col) 
	{
		if (col.gameObject.CompareTag ("Bullet"))
		{
			Destroy (col.gameObject);
			if (!isDead) {
				currentHealth--;
				if (!blood.isPlaying) {
					blood.Play ();
				}
				if (currentHealth == 0) {
					isDead = true;
				}
			}
		}
	}

	void Update(){
		if (isDead) {
			blood.Pause ();
			blood.Play ();
			gameObject.GetComponent<EnemyMovement> ().StartSinking ();
		}
	}



	/*
	public void TakeDamage (int amount)
	{
		if (isDead) {
			return;
		}

		// Play the hurt sound effect.
		enemyAudio.Play ();

		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death ();
		}
	}

	void Awake ()
	{
		// Setting up the references.
		enemyAudio = GetComponent <AudioSource> ();

		boxCollider = GetComponent <BoxCollider> ();

		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	void Death ()
	{
		isDead = true;

		boxCollider.isTrigger = true;

		enemyAudio.clip = deathClip;
		enemyAudio.Play ();

	}
	*/
}
