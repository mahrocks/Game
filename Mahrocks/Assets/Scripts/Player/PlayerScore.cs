/* This script is responsible for controlling how many instruments the player has taken */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

	private uint instrumentQuantity;
	private uint collectedInstruments;
	private bool playerWon;

	public AudioSource collectInstrumentSound;
	public AudioSource playerVictorySound;

	public Text instrumentsPickedup;
	public Text timePassed;


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
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Instrument")) {
			Destroy (other.gameObject);
			incrementCollectedInstruments ();
			increasePickedUpInstruments ();
			collectInstrumentSound.Play ();
		}
	}

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
		if (collectedInstruments == instrumentQuantity) {
			win ();
		}
	}

	public uint getCollectedInstruments ()
	{
		return collectedInstruments;
	}

	public void win ()
	{
		playerWon = true;
		playerVictorySound.Play ();
		victoryImage.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	}

	public bool haveWon ()
	{
		return playerWon;
	}

	public void increasePickedUpInstruments ()
	{
		instrumentsPickedup.text = "Instrumentos Recuperados: <color=#ffbf00>" + collectedInstruments.ToString () + "</color>";
	}

	public void increaseTimer ()
	{
		int minutes = (int)((Time.timeSinceLevelLoad / 60) - 1);
		int seconds = (int)(Time.timeSinceLevelLoad % 60);
		timePassed.text = "Time: <color=#ffbf00>" + minutes.ToString ("00") + ":" + seconds.ToString ("00") + "</color>";
		//timePassed.text = "Time: <color=#ffbf00>" + Time.timeSinceLevelLoad.ToString () + "</color>";
	}
}
