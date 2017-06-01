using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlulletSpawner : MonoBehaviour {

	public GameObject[] bullets;
	public Transform spawnPos;
	public AudioSource sound;
	public float secondsBetweenAttacks = 1.0f;
	public AudioClip[] sounds;
	private float passedTime;
	private int playingNow = 0;
	private float lastShoot = 0.0f;

	void Start(){
		passedTime = 0.0f;
	}

	void Update() {

		passedTime += Time.deltaTime;

		if (lastShoot > 2.5f) {
			playingNow = 0;
		}
		
		if (Input.GetButtonDown("Fire1") && (passedTime > secondsBetweenAttacks)) {
			lastShoot = 0.0f;

			sound.clip = sounds [playingNow];

			passedTime = 0.0f;
			GameObject bullet = bullets [Random.Range (0, bullets.Length)];
			bullet = (GameObject)Instantiate (bullet, spawnPos.position, spawnPos.transform.rotation);
			sound.Play ();
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 30;

			if (playingNow == sounds.Length - 1) {
				playingNow = 0;
			}else{
				playingNow++;
			}

			Destroy (bullet, 2.0f); 
		}

		lastShoot += Time.deltaTime;
	}
}
