using UnityEngine;
using System.Collections;

public class ReticleScript : MonoBehaviour {

	BeatManager beatz;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (beatz.beat && beatz.beatTracker % 8 == 0) {
			gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
		} else if (beatz.beat && beatz.beatTracker % 8 == 1){
			gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
		}
	}
}
