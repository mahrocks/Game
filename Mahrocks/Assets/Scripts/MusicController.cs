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

	private bool playBgMusic;

	void Awake ()
	{
		myMusic = Resources.LoadAll ("Music", typeof(AudioClip));
		playBgMusic = true;
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
		if ((!audio.isPlaying) && (playBgMusic))
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

	public void Play(){
		playBgMusic = true;
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}

	public void Pause(){
		playBgMusic = false;
		if (audio.isPlaying) {
			audio.Pause ();
		}
	}

	public void Stop() {
		playBgMusic = false;
		if (audio.isPlaying) {
			audio.Stop ();
		}
	}
}
