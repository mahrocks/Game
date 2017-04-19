using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYawnScript : MonoBehaviour
{

	public Transform target;
	public float smoothing = 5f;
	public float turnSpeed = 4.0f;

	//private bool lookAt = true;
	Quaternion rotation;

	Vector3 offset;

	void Start ()
	{
		offset = transform.position - target.position;
	}

	void FixedUpdate ()
	{
		//transform.RotateAround (transform.position, transform.right, -targetCamRot.y * turnSpeed);
		//transform.RotateAround (transform.position, Vector3.up, targetCamRot.x * turnSpeed);
		MoveCamera ();
	}

	void LateUpdate ()
	{
		TurnCamera ();
	}

	void MoveCamera ()
	{

		Vector3 targetCamPos = target.position + offset;
		//Vector3 targetCamRot = Camera.main.ScreenToViewportPoint (Input.mousePosition - target.position);
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

	void TurnCamera ()
	{
		/*if (offsetPositionSpace == Space.Self) {
			transform.position = target.TransformPoint (offsetPosition);
		} else {
			transform.position = target.position + offsetPosition;
		}*/

		// compute rotation
		/*if (lookAt) {
			transform.LookAt (target);
		} else {*/

		//NEED TO FIX THIS
		rotation.Set (Quaternion.Euler (30, 0, 0).x, target.rotation.y, 0, 0);
		transform.rotation = new Quaternion (transform.rotation.x, target.rotation.y, transform.rotation.y, transform.rotation.w);
		Debug.Log (transform.rotation); 

		//	}
	}
}
