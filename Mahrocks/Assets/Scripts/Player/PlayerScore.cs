/* This script is responsible for controlling how many instruments the player has taken */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

	private uint instrumentQuantity;
	private uint collectedInstruments;
	private bool playerWon;

	public AudioSource collectInstrumentSound;
	public AudioSource playerVictorySound;

	public RawImage victoryImage;

	// Use this for initialization
	void Awake () {
		// this values should be attributed BEFORE any call to incrementInstrumentsInGame in other scripts
		instrumentQuantity = 0;
		collectedInstruments = 0;
	}

	void Start(){
		playerWon = false;
		victoryImage.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
		if ((collectedInstruments == instrumentQuantity) && !playerWon) {
			win ();
		}
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Instrument"))
		{
			Destroy (other.gameObject);
			incremetCollectedInstruments();
			collectInstrumentSound.Play ();
		}
	}

	// When an instrument is created, it calls this method to indicate it exists.
	// This method must be called by other scripts ONLY on Start(). If it is called on Awake(),
	// instrument count value may become corrupted
	public void incrementInstrumentQuantity(){
		instrumentQuantity++;
	}

	public uint getInstrumentQuantity(){
		return instrumentQuantity;
	}

	// Called every time an instrument is picked up
	public void incremetCollectedInstruments(){
		collectedInstruments++;
	}

	public uint getCollectedInstruments(){
		return collectedInstruments;
	}

	public void win(){
		playerWon = true;
		playerVictorySound.Play ();
		victoryImage.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	}

	public bool haveWon(){
		return playerWon;
	}
}
