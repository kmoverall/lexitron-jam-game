using UnityEngine;
using System.Collections;

public class WormholeSpawner : MonoBehaviour {

	BeatManager beatz;
	LevelReader level;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		level = GameObject.FindObjectOfType<Camera>().GetComponent<LevelReader>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
