using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

	public bool isSinking;
	public bool isEmerging;
	Transform tr_Player;
	public float sinkSpeed = 2.5f;
	// The speed at which the enemy sinks through the floor when dead.
	float f_RotSpeed = 4.0f, f_MoveSpeed = 4.0f;

	// Use this for initialization
	void Start ()
	{

		tr_Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update ()
	{
		if (isSinking) {
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			//return;
		} else if (isEmerging) {
			transform.Translate (Vector3.up * sinkSpeed * Time.deltaTime);
			if (transform.position.y >= 0.5f) {
				transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
				isEmerging = false;
				GetComponent <Rigidbody> ().isKinematic = false;
			}
		}else{
			/* Look at Player*/
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (tr_Player.position - transform.position), f_RotSpeed * Time.deltaTime);

			/* Move at Player*/
			transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
		}
	}

	public void StartSinking ()
	{
		// Find and disable the Nav Mesh Agent.
		//GetComponent <NavMeshAgent> ().enabled = false;

		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;

		// The enemy should no sink.
		isSinking = true;

		// Increase the score by the enemy's score value.
		//ScoreManager.score += scoreValue;

		// After 2 seconds destory the enemy.
		Destroy (gameObject, 3f);
	}

	public void StartEmerging ()
	{
		// Find and disable the Nav Mesh Agent.
		//GetComponent <NavMeshAgent> ().enabled = false;

		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;

		// The enemy should no sink.
		isEmerging = true;

		// Increase the score by the enemy's score value.
		//ScoreManager.score += scoreValue;

	}
}

