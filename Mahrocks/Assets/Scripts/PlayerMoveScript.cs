using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
	// Invisible object player will aways face
	public Transform referencePoint;

	public GameObject enemy;
	public Transform enemyPos;

	public float spawnTime = 5.0f;

	//Vector for get the move position
	Vector3 movement;

	//time that takes swapping states
	public float speedSmoothTime = 0.1f;

	//The speed that the player will move at
	public float playerForwardSpeed = 5.0f;
	public float playerBackwardSpeed = 2.0f;
	public float playerSidewardSpeed = 2.0f;
//	public float playerMovementSpeed = 6.0f;

	Animator anim; // Reference to the animator component
//	Rigidbody playerRB;

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

	void Start ()
	{
		anim = GetComponent <Animator> ();
		//playerRB = GetComponent <Rigidbody> ();
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
		float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
		Move (h, v);
		Rotate ();
	

	}

	/*void Move(float h, float v)*/
	void Move (float horizontalAxis, float verticalAxis)
	{
		Vector2 input = new Vector2 (horizontalAxis, verticalAxis);
		Vector2 inputDir = input.normalized;


        /*movement.Set(h, 0f, v);
        movement = movement.normalized * playerMovementSpeed * Time.deltaTime;

        playerRB.MovePosition(transform.position + movement);*/

		//float verticalAxis = Input.GetAxis ("Vertical");
		float translateVertical, translateHorizontal;

		if (verticalAxis > 0) {
			translateVertical = (verticalAxis * playerForwardSpeed) * Time.deltaTime;
		} else {
			translateVertical = (verticalAxis * playerBackwardSpeed) * Time.deltaTime;
		}

		translateHorizontal = (horizontalAxis * playerSidewardSpeed) * Time.deltaTime;
		transform.Translate (translateHorizontal, 0, translateVertical);
	
		//change blend tree speed value
		float animationSpeedPercent = 1 * inputDir.magnitude;
		anim.SetFloat ("speed", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

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
		if (passedTime > spawnTime) {
			GameObject enemyCreated = Instantiate (enemy, new Vector3 ((Random.Range (1, 4) * 4.0f + transform.position.x), 0.5f, Random.Range (1, 4) * 4.0f + transform.position.z), Quaternion.LookRotation (transform.position - enemyPos.position, Vector3.up));
			enemyCreated.GetComponent<Renderer> ().material.color = colors [Random.Range (0, colors.Length)];
			enemyCreated.tag = "Enemies";
			passedTime = 0.0f;
		} else {
			passedTime += Time.deltaTime;
		}
	}
}
