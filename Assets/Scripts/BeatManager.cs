﻿using UnityEngine;
using System.Collections;

public class BeatManager : MonoBehaviour {

	float startTime;
	public int beatTracker;
	public bool beat;
	public int tempo = 124;
	public int totalBeats = 4100;
	public int countOff = 64;
	public AudioClip metronomeClick;

	public bool countingOff;

	// Use this for initialization
	void Start () {
		totalBeats = 4100;
		beatTracker = 0;
		countingOff = true;
		startTime = Time.time;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (beat) {
			beat = false;
		}

		if (Mathf.Abs(Time.time - startTime - (60/(float)tempo) * (((float)beatTracker + 1)/8)) < (Time.deltaTime / 2) + 0.0001){
			beat = true;
			beatTracker++;
			if (beatTracker % 8 == 0 && countingOff) {
				AudioSource.PlayClipAtPoint(metronomeClick, transform.position);
			}
			if (beatTracker == countOff && countingOff) {
				countingOff = false;
				Play ();
			}
		}

		if (beatTracker == totalBeats) {
			Application.LoadLevel("Game Over");
		}

	}

	void Play () {
		beat = true;
		beatTracker = 0;
		startTime = Time.time;
		gameObject.GetComponent<AudioSource>().Play();
	}

	void Stop () {
		beat = false;
		gameObject.GetComponent<AudioSource>().Stop();
	}

	void Pause () {
		gameObject.GetComponent<AudioSource>().Pause();
	}
}
