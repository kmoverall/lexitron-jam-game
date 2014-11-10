using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class WormholeRing : MonoBehaviour {

	public Transform bgQuad;
	Vector3 bgTarget;

	GameObject player;
	public PlayerControl.Lanes lane;
	public PlayerControl.SideLanes sidelane;
	Vector3 target;
	BeatManager beatz;
	double speed;
	int spawnBeat;
	Score score;
	public const int beatsToDeath = 40;
	public const int hitWindow = 2;

	bool passed;
	
	GamePadState prevState;
	GamePadState inputState;
	ButtonSet deathButtons;

	public string buttonString;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		deathButtons = new ButtonSet();

		passed = false;

		inputState = GamePad.GetState(0);
		prevState = GamePad.GetState(0);

		score = GameObject.FindObjectOfType<Camera>().GetComponent<Score>();
		
		//Dear lord this is a mess
		//Gets a random button and sets it to pressed
		//deathButtons.buttonState[deathButtons.buttonState.ElementAt((int)(Random.Range(0, deathButtons.buttonState.Count))).Key] = ButtonState.Pressed;
		
		SetKey ();
		
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		
		target = new Vector3(-20, transform.position.y, transform.position.z);
		bgTarget = new Vector3(-20, bgQuad.position.y, bgQuad.position.z);
		speed = (transform.position.x - player.transform.position.x) / (60.0 / beatz.tempo)/(beatsToDeath/8);
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
		bgQuad.position = Vector3.MoveTowards(bgQuad.position, bgTarget, step);

		if (transform.position.x <= -20) {
			Destroy(bgQuad.gameObject);
			Destroy(gameObject);
		}
		
		if (beatz.beat && beatz.beatTracker % 8 == 0) {
			gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
		} else if (beatz.beat && beatz.beatTracker % 8 == 1){
			gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
		}
		
		inputState = GamePad.GetState(0);
		
		//Destroy Enemies
		if(deathButtons.buttonHoldCompare (inputState) && 
		   deathButtons.directionHoldCompare (inputState) &&
		   beatz.beatTracker <= spawnBeat + beatsToDeath + hitWindow - 1 && 
		   beatz.beatTracker >= spawnBeat + beatsToDeath - hitWindow &&
		   lane == player.GetComponent<PlayerControl>().pos &&
		   sidelane == player.GetComponent<PlayerControl>().sidepos &&
		   !passed) {
				passed = true;
				score.Hit(15);
				gameObject.GetComponent<AudioSource>().Play ();
				
				Destroy(bgQuad.gameObject.renderer);
				Destroy(renderer);
		}
		if (!passed && beatz.beatTracker > spawnBeat + beatsToDeath + hitWindow - 1) {
			passed = true;
			score.Dodge();
		}
		
		prevState = inputState;
	}

	public void SetKey () {
		List<ButtonSet.TestButtons> tb = new List<ButtonSet.TestButtons>();
		List<ButtonSet.TestDirections> td = new List<ButtonSet.TestDirections>();

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
			case 'u':
				td.Add(ButtonSet.TestDirections.Up);
				break;
			case 'l':
				td.Add(ButtonSet.TestDirections.Left);
				break;
			case 'r':
				td.Add(ButtonSet.TestDirections.Right);
				break;
			case 'd':
				td.Add(ButtonSet.TestDirections.Down);
				break;
			}

		}
		
		foreach (ButtonSet.TestButtons b in tb) {
			deathButtons.buttonState[b] = ButtonState.Pressed;
		}

		foreach (ButtonSet.TestDirections d in td) {
			deathButtons.stickState[d] = ButtonState.Pressed;
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

		Color tmpCol = renderer.material.color;
		tmpCol.a = 0.5f;
		bgQuad.renderer.material.color = tmpCol;

		Vector3 tmpPosition = transform.position;
		if (deathButtons.stickState[ButtonSet.TestDirections.Up] == ButtonState.Pressed) {
			tmpPosition.y = player.GetComponent<PlayerControl>().topLane.position.y;
			lane = PlayerControl.Lanes.TOP;
		} else if (deathButtons.stickState[ButtonSet.TestDirections.Down] == ButtonState.Pressed) {
			tmpPosition.y = player.GetComponent<PlayerControl>().botLane.position.y;
			lane = PlayerControl.Lanes.BOT;
		} else {
			lane = PlayerControl.Lanes.MID;
		}

		if (deathButtons.stickState[ButtonSet.TestDirections.Left] == ButtonState.Pressed) {
			tmpPosition.z = player.GetComponent<PlayerControl>().topLane.position.y;
			sidelane = PlayerControl.SideLanes.LEFT;
		} else if (deathButtons.stickState[ButtonSet.TestDirections.Right] == ButtonState.Pressed) {
			tmpPosition.z = player.GetComponent<PlayerControl>().botLane.position.y;
			sidelane = PlayerControl.SideLanes.RIGHT;
		} else {
			sidelane = PlayerControl.SideLanes.MID;
		}

		transform.position = tmpPosition;
	}
}
