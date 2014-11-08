using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Transform targetLine;
	Vector3 target;
	BeatManager beatz;
	double speed;

	// Use this for initialization
	void Start () {
		targetLine = GameObject.FindGameObjectWithTag("BeatBoundary").transform;
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		target = new Vector3(-20, transform.position.y, transform.position.z);
		speed = (transform.position.x - targetLine.position.x) / (60.0 / beatz.tempo)/4;
	}
	
	// Update is called once per frame
	void Update () {
		float step = (float)speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		if (transform.position.x <= -20) {
			Destroy(gameObject);
		}

		if (beatz.beat) {
			gameObject.renderer.material.color = Color.red;
		} else if (Time.time - beatz.lastBeat >= 60.0 / (beatz.tempo*8)){
			gameObject.renderer.material.color = Color.white;
		}
	}
}
