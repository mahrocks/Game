using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour {

    Rigidbody player;
    public float playerMovementSpeed;
    public float playerRotationSpeed;    

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        float translate = (Input.GetAxis("Vertical") * playerMovementSpeed) * Time.deltaTime;
        float rotate = (Input.GetAxis("Horizontal") * playerRotationSpeed) * Time.deltaTime;

        transform.Translate(0, 0, translate);
        transform.Rotate(0, rotate, 0);
    }
}
