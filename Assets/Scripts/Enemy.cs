using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Enemy : MonoBehaviour {

	Transform targetLine;
	GameObject player;
	PlayerControl.Lanes lane;
	Vector3 target;
	BeatManager beatz;
	double speed;
	int spawnBeat;
	int beatsToDeath = 32;
	public int hitWindow = 2;

	GamePadState prevState;
	GamePadState inputState;
	ButtonSet deathButtons;

	// Use this for initialization
	void Start () {
		targetLine = GameObject.FindGameObjectWithTag("BeatBoundary").transform;
		player = GameObject.FindGameObjectWithTag("Player");
		deathButtons = new ButtonSet();

		if (transform.position.y > 0) {
			lane = PlayerControl.Lanes.TOP;
		} else if (transform.position.y < 0) {
			lane = PlayerControl.Lanes.BOT;
		} else {
			lane = PlayerControl.Lanes.MID;
		}

		inputState = GamePad.GetState(0);
		prevState = GamePad.GetState(0);

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

		inputState = GamePad.GetState(0);
		deathButtons.buttonState[ButtonSet.TestButtons.A] = ButtonState.Pressed;

		//Destroy Enemies
		if(deathButtons.buttonPressCompare (inputState, prevState) && 
		   beatz.beatTracker <= spawnBeat + beatsToDeath + hitWindow - 1 && 
		   beatz.beatTracker >= spawnBeat + beatsToDeath - hitWindow &&
		   lane == player.GetComponent<PlayerControl>().pos) {
			Destroy(gameObject);
			Debug.Log ("BLAMMO!");
		}

		prevState = inputState;
	}
}
