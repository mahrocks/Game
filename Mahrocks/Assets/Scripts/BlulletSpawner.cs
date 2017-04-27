using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlulletSpawner : MonoBehaviour {

	public GameObject[] bullets;
	public Transform spawnPos;
	public AudioSource[] sounds;

	void Update() {

		if (Input.GetButtonDown("Fire1")) {
			if (!sounds[0].isPlaying && !sounds[1].isPlaying) {

				GameObject bullet = bullets [Random.Range (0, bullets.Length)];
				bullet = (GameObject)Instantiate (bullet, spawnPos.position, spawnPos.transform.rotation);
				sounds [Random.Range (0, sounds.Length)].Play ();
				bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 15;

				// Destroy the bullet after 2 seconds
				Destroy (bullet, 2.0f); 
			}
		}
	}
}
