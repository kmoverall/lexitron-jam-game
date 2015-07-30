using UnityEngine;
using System.Collections;

public class ReticleScript : MonoBehaviour {

	BeatManager beatz;
	LevelReader level;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		level = GameObject.FindObjectOfType<Camera>().GetComponent<LevelReader>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (level.state != LevelReader.LevelState.CRUISING) {
			GetComponent<SpriteRenderer>().enabled = false;
		} else {
			GetComponent<SpriteRenderer>().enabled = true;
		}

		if (beatz.beat && beatz.beatTracker % 8 == 0) {
			gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
		} else if (beatz.beat && beatz.beatTracker % 8 == 1){
			gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
		}
	}
}
