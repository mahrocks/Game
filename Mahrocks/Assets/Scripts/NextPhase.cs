using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhase : MonoBehaviour {
	GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter (Collider other){
		
		if (other.gameObject.CompareTag ("Player")) {
			uint remaing = other.GetComponent<PlayerScore> ().getRemainingInstruments ();

			if (remaing == 0) {
				other.GetComponent<PlayerScore> ().win ();

			}
		}
	}
}
