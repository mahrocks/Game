using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
	Object[] myMusic;
	public AudioSource audio;
	public Text playing;

	private float passedTime;
	private float maxShowInfoTime = 5.0f;

	void Awake ()
	{
		myMusic = Resources.LoadAll ("Music", typeof(AudioClip));
		playRandomMusic ();
	}

	void showInfo ()
	{
		passedTime = 0.0f;
		playing.text = audio.clip.name;
	}

	void takeOffInfo ()
	{
		playing.text = "";
	}

	// Update is called once per frame
	void Update ()
	{
		if (!audio.isPlaying)
			playRandomMusic ();

		if (passedTime > maxShowInfoTime) {
			takeOffInfo ();
		} else {
			passedTime += Time.deltaTime;		
		}
	}

	void playRandomMusic ()
	{
		audio.clip = myMusic [Random.Range (0, myMusic.Length)] as AudioClip;
		audio.Play ();
		showInfo ();
	}
}
