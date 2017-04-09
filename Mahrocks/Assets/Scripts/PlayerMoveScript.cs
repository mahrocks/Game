using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
	// Invisible object player will aways face
	public Transform referencePoint;

	public GameObject enemy;
	public Transform enemyPos;

	//Vector for get the move position
	Vector3 movement;

	//The speed that the player will move at
	public float playerForwardSpeed = 10.0f;
	public float playerBackwardSpeed = 5.0f;
	public float playerSidewardSpeed = 5.0f;

	private float passedTime = 0.0f;
	private Color[] colors = new Color[] {
		Color.red,
		Color.blue,
		Color.black,
		Color.green,
		Color.gray,
		Color.yellow,
		Color.white
	};

	//The speed that the player will rotate at **
	//public float playerRotationSpeed = 0; // ** DEPRECATED ** - Player rotation speed is the same as camera rotation speed. See PlayerCameraScript.cs

	//private int floorMask;

	void Awake ()
	{
		//floorMask = LayerMask.GetMask("Floor");
		//Debug.Log ("Awaken");
	}

	void Update ()
	{
		spawnGlobalEnemies ();
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		/*float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");*/
		Move ();
		Rotate ();
	}

	/*void Move(float h, float v)*/
	void Move ()
	{
		/*
        movement.Set(h, 0f, v);
        movement = movement.normalized * playerMovementSpeed * Time.deltaTime;

        playerRB.MovePosition(transform.position + movement);
        */
		float verticalAxis = Input.GetAxis ("Vertical");
		float translateVertical, translateHorizontal;

		if (verticalAxis > 0) {
			translateVertical = (verticalAxis * playerForwardSpeed) * Time.deltaTime;
		} else {
			translateVertical = (verticalAxis * playerBackwardSpeed) * Time.deltaTime;
		}

		translateHorizontal = (Input.GetAxis ("Horizontal") * playerSidewardSpeed) * Time.deltaTime;
		transform.Translate (translateHorizontal, 0, translateVertical);
	}

	void Rotate ()
	{

		float angularX = referencePoint.transform.position.x; // reference point x axis rotation value
		float angularY = transform.position.y;                // player y axis rotation value
		float angularZ = referencePoint.transform.position.z; // reference point z axis rotation value

		Vector3 newAngularPosition = new Vector3 (angularX, angularY, angularZ);        
        
		transform.LookAt (newAngularPosition);
	}

	void spawnGlobalEnemies ()
	{
		if (passedTime > 5.0f) {
			GameObject enemyCreated = Instantiate (enemy, new Vector3 ((Random.Range (0, 3) * 2.0f + transform.position.x), 0.5f, Random.Range (0, 3) * 2.0f + transform.position.z), Quaternion.LookRotation (transform.position - enemyPos.position, Vector3.up));
			enemyCreated.GetComponent<Renderer> ().material.color = colors [Random.Range (0, colors.Length)];
			enemyCreated.tag = "Enemies";
			passedTime = 0.0f;
		} else {
			passedTime += Time.deltaTime;
		}
	}
}
