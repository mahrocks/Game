using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float delayBetweenAttacks = 0.5f;
	public int rawAttackDamage = 5;
	public float attackRange = 2.0f;

	private Animator enemyAnimator;

	private GameObject playerObject;
	private PlayerHealth player;
	private bool playerInRange;

	private SphereCollider attackCollider;
	private float timer;

	void Awake () {
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		player = playerObject.GetComponent <PlayerHealth> ();

		enemyAnimator = GetComponent <Animator> ();

		attackCollider = GetComponent <SphereCollider> ();
		attackCollider.radius = attackRange;
	}

	void Update () {
		timer += Time.deltaTime;

		if ((playerInRange) && (timer >= delayBetweenAttacks)) {
			Attack ();
		}
	}

	/* called when an object enters attack range collider */
	void OnTriggerEnter(Collider other){
		if (other.gameObject == playerObject) {
			playerInRange = true;
		}
	}

	/* called when an object exits attack range collider */
	void OnTriggerExit(Collider other){
		if (other.gameObject == playerObject) {
			playerInRange = false;
		}
	}

	void Attack() {
		timer = 0.0f;

		if (!player.isDead ()) {
			player.TakeDamage (rawAttackDamage);
		}
	}
}
