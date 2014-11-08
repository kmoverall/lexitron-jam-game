using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Transform targetLine;
	Vector3 target;
	BeatManager beatz;
	double speed;
	int spawnBeat;

	// Use this for initialization
	void Start () {
		targetLine = GameObject.FindGameObjectWithTag("BeatBoundary").transform;
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		target = new Vector3(-20, transform.position.y, transform.position.z);
		speed = (transform.position.x - targetLine.position.x) / (60.0 / beatz.tempo)/4;
		spawnBeat = beatz.beatTracker;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float step = (float)speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		if (transform.position.x <= -20) {
			Destroy(gameObject);
		}

		if (beatz.beat && beatz.beatTracker % 8 == 0) {
			gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
		} else if (beatz.beat && beatz.beatTracker % 8 == 1){
			gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
		}
	}
}
