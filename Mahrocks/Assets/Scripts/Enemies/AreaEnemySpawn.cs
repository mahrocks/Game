using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemySpawn : MonoBehaviour
{
	public GameObject enemy;
	public GameObject player;
	public Transform enemyPos;
	public int numEnemies = 0;
	public int maxEnemies = 5;

	//private float repeatRate = 1.5f;
	public float minWait = 1.0f;
	public float maxWait = 3.0f;
	private float passedTime = 0.0f;

	private EnemyMovement enemyMove;
	private bool inside = false;
	private Color[] colors = new Color[] {
		Color.red,
		Color.blue,
		Color.black,
		Color.green,
		Color.gray,
		Color.yellow,
		Color.white
	};


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			if (inside == false) {
				inside = true;
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("AreaEnemies")) {
					numEnemies++;
				}
			}
			if (numEnemies < maxEnemies) {
				if (passedTime < maxWait && passedTime > minWait) { //Between 1 and 3 seconds

					if (Random.Range (0.0f, 1.0f) >= 0.5f) { // Has 50% of spawning
						GameObject enemyCreated = Instantiate (enemy, new Vector3 ((numEnemies * 2.0f + other.transform.position.x), -0.5f, other.transform.position.z), Quaternion.LookRotation (player.transform.position - enemyPos.position, Vector3.up));
						enemyCreated.GetComponent<Renderer> ().material.color = colors [Random.Range (0, colors.Length)];
						enemyCreated.tag = "AreaEnemies";
						passedTime = 0.0f;
						enemyMove = enemyCreated.GetComponent <EnemyMovement> ();
						enemyMove.StartEmerging ();
						numEnemies++;
					}
				} else {
					if (passedTime > maxWait) { // Above 3s, spawn right away
						GameObject enemyCreated = Instantiate (enemy, new Vector3 ((numEnemies * 2.0f + other.transform.position.x), -0.5f, other.transform.position.z), Quaternion.LookRotation (player.transform.position - enemyPos.position, Vector3.up));
						enemyCreated.GetComponent<Renderer> ().material.color = colors [Random.Range (0, colors.Length)];
						enemyCreated.tag = "AreaEnemies";
						passedTime = 0.0f;
						enemyMove = enemyCreated.GetComponent <EnemyMovement> ();
						enemyMove.StartEmerging ();
						numEnemies++;

					} else { //Or just add the time
						passedTime += Time.deltaTime;
					}
				}
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			inside = false;
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("AreaEnemies")) {
				enemyMove = g.GetComponent <EnemyMovement> ();
				enemyMove.StartSinking ();
			}
			numEnemies = 0;
		}
	}
	

}
