using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	int score;
	int multiplier;
	int health;
	int streak;

	BeatManager beatz;

	ScoreTracker tracker;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		tracker = GameObject.FindGameObjectWithTag("ScoreTracker").GetComponent<ScoreTracker>();
		score = 0;
		multiplier = 1;
		health = 100;
	}

	void OnGUI () {
		GUI.Label (new Rect (25, 25, 100, 30), "Score: " + score);
		GUI.Label (new Rect (25, 45, 100, 30), "x" + multiplier);
		GUI.Label (new Rect (25, 65, 100, 30), "Streak: " + streak);
		GUI.Label (new Rect (25, 85, 100, 30), "Health: " + health);
	}

	void FixedUpdate () {
		tracker.score = score;
		tracker.percentage = (100 * beatz.beatTracker) / beatz.totalBeats;
		if (health <= 0) {
			Application.LoadLevel("Game Over");
		}
	}

	public void GotHit () {
		health -= 20;
		multiplier = 1;
		streak = 0;
	}

	public void Dodge () {
		if (multiplier > 1)
			multiplier--;
		streak = 0;
	}

	public void Hit (int points) {
		score += points*multiplier;
		streak++;
		if (health < 99) {
			health += 2;
		} else if (health == 99) {
			health++;
		}
		if(streak % 10 == 0 && multiplier < 10) {
			multiplier++;
		}
	}
}
