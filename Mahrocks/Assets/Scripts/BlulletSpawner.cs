using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlulletSpawner : MonoBehaviour {

	public GameObject[] bullets;
	public Transform spawnPos;
	public AudioSource[] sounds;
	public float secondsBetweenAttacks = 1.0f;

	private float passedTime;

	void Start(){
		passedTime = 0.0f;
	}

	void Update() {

		passedTime += Time.deltaTime;
	
		if (Input.GetButtonDown("Fire1") && (passedTime > secondsBetweenAttacks)) {
			//if (!sounds[0].isPlaying && !sounds[1].isPlaying) {

			passedTime = 0.0f;
			GameObject bullet = bullets [Random.Range (0, bullets.Length)];
			bullet = (GameObject)Instantiate (bullet, spawnPos.position, spawnPos.transform.rotation);
			sounds [Random.Range (0, sounds.Length)].Play ();
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 30;

			// Destroy the bullet after 2 seconds
			Destroy (bullet, 2.0f); 
			//}
		}
	}
}
