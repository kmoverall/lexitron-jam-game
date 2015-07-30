using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XInputDotNetPure;

public class Enemy : MonoBehaviour {

	Transform targetLine;

    public Transform explosionParticle;
    public Transform playerExplosionParticle;
    public Transform engineParticle;

    public int points;

	GameObject player;
	public PlayerControl.Lanes lane;
	Vector3 target;
	BeatManager beatz;
	Score score;
	double speed;
	int spawnBeat;
	public const int beatsToDeath = 40;
	public const int hitWindow = 2;

	public Sprite[] shipSprites = new Sprite[6];

	public AudioClip playerHitSound;

	bool passed;

	GamePadState prevState;
	GamePadState inputState;
	ButtonSet deathButtons;

	public string buttonString;

	// Use this for initialization
	void Start () {

		passed = false;

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
		score = GameObject.FindObjectOfType<Camera>().GetComponent<Score>();
        
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
			gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
		} else if (beatz.beat && beatz.beatTracker % 8 == 1){
			gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
		}

		inputState = GamePad.GetState(0);

		//Destroy Enemies
		if(deathButtons.buttonPressCompare (inputState, prevState) && 
		   beatz.beatTracker <= spawnBeat + beatsToDeath + hitWindow - 1 && 
		   beatz.beatTracker >= spawnBeat + beatsToDeath - hitWindow &&
		   lane == player.GetComponent<PlayerControl>().pos &&
            !passed) {
               Vector3 laserPos = player.transform.position;
               laserPos.x += player.transform.localScale.x / 2;
			   Instantiate(explosionParticle, transform.position, Quaternion.Euler(-90, 0, 0));
			   Destroy(renderer);
               Destroy(engineParticle.gameObject);
			   passed = true;
			   score.Hit(points);
        }

		if (!passed && beatz.beatTracker == spawnBeat + beatsToDeath + 8) {
			if (lane == player.GetComponent<PlayerControl>().pos) {
				score.GotHit();
                Instantiate(playerExplosionParticle, player.transform.position, Quaternion.Euler(0, -90, 0));
				Destroy(renderer);
                Destroy(engineParticle.gameObject);
				passed = true;
			} else {
				passed = true;
				score.Dodge();
			}
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
			gameObject.GetComponent<SpriteRenderer>().sprite = shipSprites[0];
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.B] == ButtonState.Pressed) {
			gameObject.GetComponent<SpriteRenderer>().sprite = shipSprites[1];
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.X] == ButtonState.Pressed) {
			gameObject.GetComponent<SpriteRenderer>().sprite = shipSprites[2];
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.Y] == ButtonState.Pressed) {
			gameObject.GetComponent<SpriteRenderer>().sprite = shipSprites[3];
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.L] == ButtonState.Pressed) {
			gameObject.GetComponent<SpriteRenderer>().sprite = shipSprites[4];
		}
		if (deathButtons.buttonState[ButtonSet.TestButtons.R] == ButtonState.Pressed) {
			gameObject.GetComponent<SpriteRenderer>().sprite = shipSprites[5];
		}
	}
}
