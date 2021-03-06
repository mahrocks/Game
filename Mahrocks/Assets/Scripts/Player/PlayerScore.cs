﻿/* This script is responsible for controlling how many instruments the player has taken */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{

	private uint instrumentQuantity;
	private uint collectedInstruments;
	private bool playerWon;

	public AudioSource collectInstrumentSound;
	public AudioSource playerVictorySound;
	private MusicController bgMusic;

	public Text instrumentsPickedup;
	public Text timePassed;

	public GlobalEnemySpawn gespawn;

	public GameObject[] gateParts;
	//public GameObject gamespawner;
	public RawImage victoryImage;

	// Use this for initialization
	void Awake ()
	{
		// this values should be attributed BEFORE any call to incrementInstrumentsInGame in other scripts
		instrumentQuantity = 0;
		collectedInstruments = 0;
	}

	void Start ()
	{
		
		//gespawn = (GlobalEnemySpawn)gamespawner.GetComponent (typeof(GlobalEnemySpawn));
		gespawn = GameObject.FindObjectOfType (typeof(GlobalEnemySpawn)) as GlobalEnemySpawn;
		bgMusic = GameObject.FindGameObjectWithTag ("BgMusic").GetComponent<MusicController>();
		playerWon = false;
		victoryImage.color = Color.clear;
		instrumentsPickedup.text = "Instrumentos Recuperados: <color=#ffbf00> 0</color>";
		timePassed.text = "Time: <color=#ffbf00> 00:00 </color>";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!playerWon) {
			increaseTimer ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("Phase1Scene");
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("MenuScene");
		}
	}

	/*private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Instrument")) {
			Destroy (other.gameObject);
			incrementCollectedInstruments ();
			increasePickedUpInstruments ();
			collectInstrumentSound.Play ();
			gespawn.reduceTime ();
		}
	}*/

	// When an instrument is created, it calls this method to indicate it exists.
	// This method must be called by other scripts ONLY on Start(). If it is called on Awake(),
	// instrument count value may become corrupted
	public void incrementInstrumentQuantity ()
	{
		instrumentQuantity++;
	}

	public uint getInstrumentQuantity ()
	{
		return instrumentQuantity;
	}

	// Called every time an instrument is picked up
	public void incrementCollectedInstruments ()
	{
		collectedInstruments++;
		collectInstrumentSound.Play ();
		gespawn.reduceTime ();
		instrumentsPickedup.text = "Instrumentos Recuperados: <color=#ffbf00>" + collectedInstruments.ToString () + "</color>";
		if (collectedInstruments == instrumentQuantity) {
			var rotateVect = gateParts [0].transform.rotation.eulerAngles;
			rotateVect.y = 60.0f;
			gateParts [0].transform.rotation = Quaternion.Euler (rotateVect);

			rotateVect = gateParts [1].transform.rotation.eulerAngles;
			rotateVect.y = -60.0f;
			gateParts [1].transform.rotation = Quaternion.Euler (rotateVect);

			var positionVect = new Vector3 (2.0f, 0.0f, 5.0f);

			gateParts [0].transform.position += positionVect;

			positionVect = new Vector3 (-2.0f, 0.0f, 5.0f);

			gateParts [1].transform.position += positionVect;
		}
	}

	public uint getCollectedInstruments ()
	{
		return collectedInstruments;
	}

	public uint getRemainingInstruments(){
		return instrumentQuantity - collectedInstruments;
	}

	public void win ()
	{
		bgMusic.Stop ();
		playerWon = true;
		playerVictorySound.Play ();
		victoryImage.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	}

	public bool haveWon ()
	{
		return playerWon;
	}

	/*public void increasePickedUpInstruments ()
	{
		instrumentsPickedup.text = "Instrumentos Recuperados: <color=#ffbf00>" + collectedInstruments.ToString () + "</color>";
	}*/

	public void increaseTimer ()
	{
		int minutes = (int)((Time.timeSinceLevelLoad / 60));
		int seconds = (int)(Time.timeSinceLevelLoad % 60);
		timePassed.text = "Time: <color=#ffbf00>" + minutes.ToString ("00") + ":" + seconds.ToString ("00") + "</color>";
		//timePassed.text = "Time: <color=#ffbf00>" + Time.timeSinceLevelLoad.ToString () + "</color>";
	}
}
