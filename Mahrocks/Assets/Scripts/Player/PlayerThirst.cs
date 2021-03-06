﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThirst : MonoBehaviour {
	
	public int startingBeer = 100;
	public Slider beerSlider;
	public RawImage thirstImage;
	public AudioSource playerThirstSound;
	public AudioSource playerBurpSound;


	public float secondsBetweenThirstDrops = 1.0f;
	public int thirstAmountToLose = 1;
	public float thirstTierChangeSpeed = 2.0f;


	private int currentBeer;
	private PlayerHealth player;
	private float timer;

	public Color thirstLevel1Colour = new Color (1.0f, 1.0f, 0.0f, 0.3f);
	public Color thirstLevel2Colour = new Color (1.0f, 1.0f, 0.0f, 0.6f);

	private uint currentThirstLevel;
	private uint previousThirstLevel;

	private GameObject playerObject;
	private PlayerScore pscore;


	void Awake () {
		player = GetComponent <PlayerHealth> ();
	}

	void Start ()
	{
		timer = 0.0f;
		currentBeer = startingBeer;

		currentThirstLevel = 0;
		previousThirstLevel = 0;
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		pscore = playerObject.GetComponent <PlayerScore> ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer >= secondsBetweenThirstDrops && !player.isDead() && !pscore.haveWon()) {
			timer = 0.0f;
			LoseBeer(thirstAmountToLose);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Chopp"))
		{
			Destroy (other.gameObject);
			RecoverBeer (Random.Range (20 , 30));
			playerBurpSound.Play ();

		}
	}

	private void LoseBeer (int amount) {
		currentBeer -= amount;
		beerSlider.value = currentBeer;
		CheckThirstLevel ();
	}

	public void RecoverBeer (int amount) {
		currentBeer += amount;
		if (currentBeer > 100) {
			currentBeer = 100;
		}
		beerSlider.value = currentBeer;
	}

	private void CheckThirstLevel () {
		previousThirstLevel = currentThirstLevel;

		// change thirst image
		if (currentBeer >= 66) {
			currentThirstLevel = 0;
			thirstImage.color = Color.clear;
		} else if (currentBeer >= 33) {
			currentThirstLevel = 1;
			thirstImage.color = thirstLevel1Colour;
		} else {
			currentThirstLevel = 2;
			thirstImage.color = thirstLevel2Colour;
		}

		// play Sound
		if (currentThirstLevel != previousThirstLevel) {
			playerThirstSound.Play ();
		}

		// kill Player
		if (currentBeer <= 0 && !player.isDead() && !pscore.haveWon()) {
			thirstImage.color = Color.clear;
			player.Die ();
		}

	}
}
