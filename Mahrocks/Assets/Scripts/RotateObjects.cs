using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour {
	public int axis_x, axis_y, axis_z;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (30*axis_x, 30*axis_y,30*axis_z) * Time.deltaTime);

	}
}
