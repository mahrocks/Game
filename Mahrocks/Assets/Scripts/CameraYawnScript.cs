using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYawnScript : MonoBehaviour
{

	public Transform target;
	public float smoothing = 5f;
	public float turnSpeed = 4.0f;


	Vector3 offset;

	void Start ()
	{
		offset = transform.position - target.position;
	}

	void FixedUpdate ()
	{
		Vector3 targetCamPos = target.position + offset;
		Vector3 targetCamRot = Camera.main.ScreenToViewportPoint (Input.mousePosition - target.position);
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		transform.RotateAround (transform.position, transform.right, -targetCamRot.y * turnSpeed);
		transform.RotateAround (transform.position, Vector3.up, targetCamRot.x * turnSpeed);
	}

}
