using UnityEngine;
using System.Collections;

public class BeatManager : MonoBehaviour {

	float startTime;
	public int beatTracker;
	public bool beat;
	public int tempo;

	// Use this for initialization
	void Start () {
		tempo = 124;
		beatTracker = 0;
		Play();
	}

	void Awake () {
		Application.targetFrameRate = 300;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (beat) {
			beat = false;
		}

		if (Mathf.Abs(Time.time - startTime - (60/(float)tempo) * (((float)beatTracker + 1)/8)) < (Time.deltaTime / 2) + 0.0001){
			beat = true;
			beatTracker++;
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
