using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public float delayBetweenAttacks = 0.5f;
	public int rawAttackDamage = 1;

	private GameObject playerObject;
	private GameObject enemyObject;
	private PlayerHealth player;
	private EnemyLife enemy;
	private bool enemyInRange;

	private BoxCollider attackCollider;
	private float timer;

	// Use this for initialization
	void Awake ()
	{
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		player = playerObject.GetComponent <PlayerHealth> ();

		//enemyAnimator = GetComponent <Animator> ();

		attackCollider = GetComponent <BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == playerObject) {
			enemyInRange = true;
		}
	}

	/* called when an object exits attack range collider */
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == playerObject) {
			enemyInRange = false;
		}
	}

	void Attack ()
	{
		timer = 0.0f;

		if (!player.isDead ()) {
			enemy.TakeDamage (rawAttackDamage);
		}
	}
}
