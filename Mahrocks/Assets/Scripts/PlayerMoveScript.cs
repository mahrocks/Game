using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{

	Rigidbody playerRB;
	//Reference to the player's rigidbody
	Vector3 movement;
	//Vector for get the move position

	public float playerMovementSpeed;
	//The speed that the player will move at
	public float playerRotationSpeed;
	//The speed that the player will rotate at

	int floorMask;

	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		playerRB = GetComponent<Rigidbody> ();
		Debug.Log ("Awaken");
	}

	// Update is called once per frame
	void FixedUpdate ()
	{

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();

		//float translate = (Input.GetAxis("Vertical") * playerMovementSpeed) * Time.deltaTime;
		//float rotate = (Input.GetAxis("Horizontal") * playerRotationSpeed) * Time.deltaTime;
		//transform.Translate (0, 0, translate);
		//transform.Rotate (0, rotate, 0);
	}

	void Move (float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * playerMovementSpeed * Time.deltaTime;

		playerRB.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRB.MoveRotation (newRotation);
		}
	}
}
