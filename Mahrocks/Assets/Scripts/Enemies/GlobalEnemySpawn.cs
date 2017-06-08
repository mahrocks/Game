using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnemySpawn : MonoBehaviour
{

	private float spawnTime = 7.0f;

	public GameObject enemy;
	public Transform playerPos;

	private EnemyMovement enemyMove;

	private float passedTime;
	private Color[] colors = new Color[] {
		Color.red,
		Color.blue,
		Color.black,
		Color.green,
		Color.gray,
		Color.yellow,
		Color.white
	};

	// Use this for initialization
	void Start ()
	{
		passedTime = 0.0f;
	}

	void Update ()
	{
		spawnGlobalEnemies ();
	}

	void spawnGlobalEnemies ()
	{
		if (passedTime > spawnTime) {
			GameObject newEnemy = Instantiate (enemy, new Vector3 ((Random.Range (4, 8) * 4.0f + playerPos.position.x), -0.1f, Random.Range (4, 8) * 4.0f + playerPos.position.z), Quaternion.LookRotation (playerPos.position, Vector3.up));
			newEnemy.GetComponent<Renderer> ().material.color = colors [Random.Range (0, colors.Length)];
			enemyMove = newEnemy.GetComponent <EnemyMovement> ();
			enemyMove.StartEmerging ();
			newEnemy.tag = "Enemies";
			passedTime = 0.0f;
		} else {
			passedTime += Time.deltaTime;
		}
	}

	public void reduceTime ()
	{
		spawnTime -= 0.5f;
	}
}
