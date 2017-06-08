using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemySpawn : MonoBehaviour
{
	public GameObject enemy;
	public GameObject player;
	public GameObject enemyPos;
	public int maxEnemies = 5;

	//private float repeatRate = 1.5f;
	public float minWait = 1.0f;
	public float maxWait = 3.0f;
	private float passedTime = 0.0f;

	private EnemyMovement enemyMove;
	private bool inside = false;
	private int numEnemies = 0;
	private int areaEnemiesKilled = 0;
	private int enemiesLeft;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		enemiesLeft = maxEnemies;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			/*if (inside == false) {
				inside = true;
				foreach (GameObject g in GameObject.FindGameObjectsWithTag("AreaEnemies")) {
					numEnemies++;
				}
			}*/
			if (numEnemies < enemiesLeft) {
				if (passedTime < maxWait && passedTime > minWait) { //Between 1 and 3 seconds

					if (Random.Range (0.0f, 1.0f) >= 0.5f) { // Has 50% chance of spawning
						spawnNewEnemy (enemyPos);
						passedTime = 0.0f;
					}
				} else {
					if (passedTime > maxWait) { // Above 3s, spawn right away
						spawnNewEnemy (enemyPos);
						passedTime = 0.0f;

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

	private void spawnNewEnemy(GameObject spawnAreaSpawner){
		GameObject enemyCreated = Instantiate (enemy, 
											   new Vector3 ((numEnemies * 2.0f + spawnAreaSpawner.transform.position.x), -0.5f, spawnAreaSpawner.transform.position.z), 
											   Quaternion.LookRotation (player.transform.position - enemyPos.transform.position, Vector3.up));
		enemyCreated.tag = "AreaEnemies";
		enemyCreated.transform.parent = this.gameObject.transform;
		enemyMove = enemyCreated.GetComponent <EnemyMovement> ();
		enemyMove.StartEmerging ();
		numEnemies++;
	}

	public int getAreaMaxEnemies() {
		return maxEnemies;
	}

	public int getAreaEnemiesKilled() {
		return areaEnemiesKilled;
	}

	public int getEnemiesLeft (){
		return enemiesLeft;
	}

	public void increaseAreaEnemiesKilled() {
		areaEnemiesKilled++;
		enemiesLeft--;
		numEnemies--;
	}

}
