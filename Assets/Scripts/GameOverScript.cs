using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GameOverScript : MonoBehaviour {

	ScoreTracker tracker;
	GUIStyle style;
	float sceneLoad;

	int selectedIndex = 0;

	GamePadState inputState;
	GamePadState prevState;

	// Use this for initialization
	void Start () {
		tracker = GameObject.FindGameObjectWithTag("ScoreTracker").GetComponent<ScoreTracker>();
		sceneLoad = Time.time;
		inputState = GamePad.GetState(0);
		prevState = GamePad.GetState(0);
	}

	void OnGUI () {
		style = GUI.skin.GetStyle("Label");
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;
		if (tracker.percentage < 100) {
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 - 100, 100, 50), "GAME OVER", style);
			GUI.Label (new Rect (Screen.width/2 - 150, Screen.height/2 - 70, 300, 50), "That day, fresh beats went silent in the Groove Galaxy.", style);
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 - 40, 100, 50), tracker.percentage + "%", style);
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 - 10, 100, 50), "Score: "+tracker.score, style);
		} else {
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 - 100, 100, 50), "SUCCESS", style);
			GUI.Label (new Rect (Screen.width/2 - 150, Screen.height/2 - 70, 300, 50), "The Groove Galaxy is safe to jam another day.", style);
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 - 40, 100, 50), "100%", style);
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 - 10, 100, 50), "Score: "+tracker.score, style);
		}

		if (selectedIndex == 0) {
			style.normal.textColor = Color.yellow;
		}
		else {
			style.normal.textColor = Color.white;
		}            
		GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 + 50, 100, 50), "Play Again", style);
		if (selectedIndex == 1) {
			style.normal.textColor = Color.yellow;
		}
		else {
			style.normal.textColor = Color.white;
		}   
		GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 + 80, 100, 50), "Return to Main Menu", style);
	}

	// Update is called once per frame
	void Update () {
		inputState = GamePad.GetState(0);

		if (inputState.DPad.Up == ButtonState.Pressed && prevState.DPad.Up != ButtonState.Pressed && selectedIndex > 0) {
			selectedIndex--;
		}
		if (inputState.DPad.Down == ButtonState.Pressed && prevState.DPad.Down != ButtonState.Pressed && selectedIndex < 1) {
			selectedIndex++;
		}

		if (inputState.Buttons.X == ButtonState.Pressed && prevState.Buttons.X != ButtonState.Pressed) {
			if (selectedIndex == 0) {
				Application.LoadLevel("Cruise");
			} else {
				Application.LoadLevel("Main Menu");
			}
		}

		prevState = inputState;
	}
}
