using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
	private float nightspeed = 2f;
	private float starsspeed = 0.7f;
	private ParticleSystem teste;

	public GameObject map;
	public GameObject stars;
	// Use this for initialization
	void Start ()
	{
		map = GameObject.FindGameObjectWithTag ("Ground");
		stars = GameObject.FindGameObjectWithTag ("Stars");
	}

	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround (map.transform.position, Vector3.forward, nightspeed * Time.deltaTime);
		stars.transform.RotateAround (map.transform.position, Vector3.forward, starsspeed * Time.deltaTime);
		transform.LookAt (map.transform.position);
	}
}