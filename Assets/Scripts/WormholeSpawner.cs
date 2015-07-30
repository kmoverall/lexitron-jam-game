using UnityEngine;
using System.Collections;

public class WormholeSpawner : MonoBehaviour {

	BeatManager beatz;

	public Transform spawnRing;
	public Transform spawnQuad;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
	}
	
	// Update is called once per frame
	public void Spawn (string k) {
		Transform newRing = (Transform)Instantiate(spawnRing, gameObject.transform.position, Quaternion.Euler(0, 90, 0));
		Transform newQuad = (Transform)Instantiate(spawnQuad, gameObject.transform.position, Quaternion.Euler(0, 90, 0));
		newRing.GetComponent<WormholeRing>().buttonString = k;
		newRing.GetComponent<WormholeRing>().bgQuad = newQuad;
	}
}
