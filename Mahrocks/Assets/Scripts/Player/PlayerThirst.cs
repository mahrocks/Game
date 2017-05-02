using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThirst : MonoBehaviour {
	
	public int startingBeer = 100;
	public Slider beerSlider;
	public RawImage thirstImage;
	public AudioSource playerThirstSound;

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

	void Awake () {
		player = GetComponent <PlayerHealth> ();
	}

	void Start ()
	{
		timer = 0.0f;
		currentBeer = startingBeer;

		currentThirstLevel = 0;
		previousThirstLevel = 0;
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer >= secondsBetweenThirstDrops && !player.isDead()) {
			timer = 0.0f;
			LoseBeer(thirstAmountToLose);
		}
	}

	private void LoseBeer (int amount) {
		currentBeer -= amount;
		beerSlider.value = currentBeer;
		CheckThirstLevel ();
	}

	public void RecoverBeer (int amount) {
		currentBeer += amount;
		if (currentBeer > 100 && !player.isDead()) {
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
		if (currentBeer <= 0 && !player.isDead()) {
			thirstImage.color = Color.clear;
			player.Die ();
		}

	}
}
