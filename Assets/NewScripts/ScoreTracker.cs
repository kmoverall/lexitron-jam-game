using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	int score;
	int percentage;

    public int Score { get { return score; } set { score = value; } }
    public int Percentage { get { return percentage;  } set { percentage = value; } }

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}
}
