using UnityEngine;
using System.Collections;

public class BeatManager : MonoBehaviour {

	public float lastBeat;
	int beatTracker;
	public bool beat;
	public int tempo;

	// Use this for initialization
	void Start () {
		tempo = 124;
		Play();
	}

	// Update is called once per frame
	void Update () {
		if (beat) {
			beat = false;
		}

		if (Time.time - lastBeat >= 60.0 / tempo){
			beat = true;
			lastBeat = Time.time;
        }

	}

	void Play () {
		beat = true;
		beatTracker = 0;
		lastBeat = Time.time;
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
