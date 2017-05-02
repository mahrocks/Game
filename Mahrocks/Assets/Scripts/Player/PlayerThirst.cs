using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThirst : MonoBehaviour {
	
	public int startingBeer = 100;
	public Slider beerSlider;
	public RawImage thirstImage;

	public float secondsBetweenThirstDrops = 1.0f;
	public int thirstAmountToLose = 1;
	public float thirstTierChangeSpeed = 2.0f;

	private int currentBeer;
	private PlayerHealth player;
	private float timer;

	public Color thirstTier1Colour = new Color (1.0f, 1.0f, 0.0f, 0.3f);
	public Color thirstTier2Colour = new Color (1.0f, 1.0f, 0.0f, 0.6f);


	//AudioSource playerThirstSound;

	void Awake () {
		player = GetComponent <PlayerHealth> ();
	}

	void Start ()
	{
		timer = 0.0f;
		currentBeer = startingBeer;
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer >= secondsBetweenThirstDrops) {
			timer = 0.0f;
			LoseBeer(thirstAmountToLose);
		}
	}

	private void LoseBeer (int amount) {
		currentBeer -= amount;
		beerSlider.value = currentBeer;
		if (currentBeer <= 0 && !player.isDead()) {
			player.Die ();
		}
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
		if (currentBeer >= 66) {
			thirstImage.color = Color.clear;
		} else if (currentBeer >= 33) {
			thirstImage.color = thirstTier1Colour;
		} else {
			thirstImage.color = thirstTier2Colour;
		}
	}
}
