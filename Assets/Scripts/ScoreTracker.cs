using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	public int score;
	public int percentage;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}
}
