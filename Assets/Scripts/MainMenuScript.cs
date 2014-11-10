using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class MainMenuScript : MonoBehaviour {

	GUIStyle style;

	GamePadState prevState;
	GamePadState inputState;
    
	public enum MenuState {MAIN, HELP, SONG, DIFF};
	Stack<MenuState> state;
	int selectedIndex = 0;
	int maxIndex = 2;

	// Use this for initialization
	void Start () {
		state = new Stack<MenuState>();
		state.Push (MenuState.MAIN);
		Screen.showCursor = false;
		inputState = GamePad.GetState(0);
		prevState = GamePad.GetState(0);
    }

	void OnGUI () {
		style = GUI.skin.GetStyle("Label");
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 - 100, 100, 50), "Space BEATallion", style);

		switch (state.Peek ()) {
		case MenuState.MAIN:
			if (selectedIndex == 0) {
				style.normal.textColor = Color.yellow;
			}
			else {
				style.normal.textColor = Color.white;
			}            
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2, 100, 50), "Start", style);

			if (selectedIndex == 1) {
				style.normal.textColor = Color.yellow;
			}
			else {
				style.normal.textColor = Color.white;
            }
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 + 50, 100, 50), "Help", style);

			if (selectedIndex == 2) {
				style.normal.textColor = Color.yellow;
			}
			else {
				style.normal.textColor = Color.white;
            }
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2 + 100, 100, 50), "Quit", style);
			break;
		case MenuState.SONG:
			if (selectedIndex == 0) {
				style.normal.textColor = Color.yellow;
			}
			else {
				style.normal.textColor = Color.white;
			}            
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2, 100, 50), "Cephalopod", style);
            break;
		case MenuState.DIFF:
			if (selectedIndex == 0) {
				style.normal.textColor = Color.yellow;
			}
			else {
				style.normal.textColor = Color.white;
			}            
			GUI.Label (new Rect (Screen.width/2 - 50, Screen.height/2, 100, 50), "Easy", style);
            break;
		case MenuState.HELP:
			break;
        }
        
        
    }
    
    // Update is called once per frame
	void Update () {
		inputState = GamePad.GetState(0);

		if (inputState.DPad.Up == ButtonState.Pressed && prevState.DPad.Up != ButtonState.Pressed && selectedIndex > 0) {
			selectedIndex--;
		}
		if (inputState.DPad.Down == ButtonState.Pressed && prevState.DPad.Down != ButtonState.Pressed && selectedIndex < maxIndex) {
			selectedIndex++;
        }
		if (inputState.Buttons.X == ButtonState.Pressed && prevState.Buttons.X != ButtonState.Pressed) {
	        switch (state.Peek()) {
			case MenuState.MAIN:
				if (selectedIndex == 0) {
					state.Push (MenuState.SONG);
					selectedIndex = 0;
				} else if (selectedIndex == 1) {
					state.Push(MenuState.HELP);
					selectedIndex = 1;
				} else if (selectedIndex == 2) {
					UnityEditor.EditorApplication.isPlaying = false;
					Application.Quit();
	            }
				break;
			case MenuState.SONG:
				state.Push(MenuState.DIFF);
				selectedIndex = 0;
				break;
			case MenuState.DIFF:
				Application.LoadLevel("Cruise");
				break;
			case MenuState.HELP:
				state.Pop ();
				selectedIndex = 1;
				break;
	        }
		}

		if (inputState.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y != ButtonState.Pressed && state.Peek() != MenuState.MAIN){
			state.Pop ();

		}
		prevState = inputState;
	}
}
