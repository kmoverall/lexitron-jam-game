using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XInputDotNetPure;

public class Enemy : MonoBehaviour {

	Transform targetLine;
	GameObject player;
	public PlayerControl.Lanes lane;
	Vector3 target;
	BeatManager beatz;
	double speed;
	int spawnBeat;
	public const int beatsToDeath = 40;
	public const int hitWindow = 2;

	GamePadState prevState;
	GamePadState inputState;
	ButtonSet deathButtons;

	public string buttonString;

	// Use this for initialization
	void Start () {
		targetLine = GameObject.FindGameObjectWithTag("BeatBoundary").transform;
		player = GameObject.FindGameObjectWithTag("Player");
		deathButtons = new ButtonSet();

		inputState = GamePad.GetState(0);
		prevState = GamePad.GetState(0);
	
		//Dear lord this is a mess
		//Gets a random button and sets it to pressed
		//deathButtons.buttonState[deathButtons.buttonState.ElementAt((int)(Random.Range(0, deathButtons.buttonState.Count))).Key] = ButtonState.Pressed;

		SetKey ();

		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
        
        target = new Vector3(-20, transform.position.y, transform.position.z);
		speed = (transform.position.x - targetLine.position.x) / (60.0 / beatz.tempo)/(beatsToDeath/8);
		if (!beatz.countingOff) {
			spawnBeat = beatz.beatTracker;
		} else {
			spawnBeat = beatz.beatTracker - beatz.countOff;
		}
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

		//Destroy Enemies
		if(deathButtons.buttonPressCompare (inputState, prevState) && 
		   beatz.beatTracker <= spawnBeat + beatsToDeath + hitWindow - 1 && 
		   beatz.beatTracker >= spawnBeat + beatsToDeath - hitWindow &&
		   lane == player.GetComponent<PlayerControl>().pos) {
			gameObject.GetComponent<AudioSource>().Play ();
			Destroy(renderer);
		}

		prevState = inputState;
	}

	public void SetKey () {
		List<ButtonSet.TestButtons> tb = new List<ButtonSet.TestButtons>();
		foreach (char c in buttonString) {
			switch (c) {
			case 'A':
				tb.Add(ButtonSet.TestButtons.A);
				break;
			case 'B':
				tb.Add(ButtonSet.TestButtons.B);
	            break;
			case 'X':
				tb.Add(ButtonSet.TestButtons.X);
	            break;
			case 'Y':
				tb.Add(ButtonSet.TestButtons.Y);
	            break;
			case 'L':
				tb.Add(ButtonSet.TestButtons.L);
	            break;
			case 'R':
				tb.Add(ButtonSet.TestButtons.R);
	            break;
	        }
		}

		foreach (ButtonSet.TestButtons b in tb) {
			deathButtons.buttonState[b] = ButtonState.Pressed;
		}


		SetColor ();
	}

	void SetColor () {
		if (deathButtons.buttonState[ButtonSet.TestButtons.A] == ButtonState.Pressed) {
			renderer.material.color = Color.green;
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.B] == ButtonState.Pressed) {
			renderer.material.color = Color.red;
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.X] == ButtonState.Pressed) {
			renderer.material.color = Color.blue;
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.Y] == ButtonState.Pressed) {
			renderer.material.color = Color.yellow;
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.L] == ButtonState.Pressed) {
			renderer.material.color = Color.white;
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.R] == ButtonState.Pressed) {
			renderer.material.color = Color.gray;
		}
	}
}
