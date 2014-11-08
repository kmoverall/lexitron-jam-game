﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	BeatManager beatz;
	public Transform spawnEnemy;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (beatz.beat && beatz.beatTracker % 8 == 0) {
			Instantiate(spawnEnemy, gameObject.transform.position, Quaternion.identity);
		}
	}
}
