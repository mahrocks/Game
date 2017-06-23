using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
	private float dayspeed = 5f;

	public GameObject map;
	// Use this for initialization
	void Start ()
	{
		map = GameObject.FindGameObjectWithTag ("Ground");
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround (map.transform.position, Vector3.forward, dayspeed * Time.deltaTime);
		transform.LookAt (map.transform.position);
	}
}
