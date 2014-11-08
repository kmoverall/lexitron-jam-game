using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class InputTestScript : MonoBehaviour {

	GamePadState state;
	PlayerIndex playerIndex;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; ++i)
		{
			PlayerIndex testPlayerIndex = (PlayerIndex)i;
			GamePadState testState = GamePad.GetState(testPlayerIndex);
			if (testState.IsConnected)
			{
				Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
				playerIndex = testPlayerIndex;
			}
		}
	}
	
	// Update is called once per frame
    void Update () {
        
        
		state = GamePad.GetState(playerIndex);

		if (state.Buttons.A == ButtonState.Pressed) {
			Debug.Log("A");
        }

		if (state.Buttons.B == ButtonState.Pressed) {
			Debug.Log("B");
        }

		if (state.Buttons.X == ButtonState.Pressed) {
			Debug.Log("X");
        }

		if (state.Buttons.Y == ButtonState.Pressed) {
			Debug.Log("Y");
        }

		if (state.DPad.Up == ButtonState.Pressed) {
			Debug.Log("Up");
        }

		if (state.DPad.Left == ButtonState.Pressed) {
			Debug.Log("Left");
        }
        
		if (state.DPad.Right == ButtonState.Pressed) {
			Debug.Log("Right");
        }

		if (state.DPad.Down == ButtonState.Pressed) {
			Debug.Log("Down");
        }

		if (state.Buttons.LeftShoulder == ButtonState.Pressed) {
			Debug.Log("LB");
        }

		if (state.Buttons.RightShoulder == ButtonState.Pressed) {
			Debug.Log("RB");
        }

		if (state.Triggers.Right != 0) {
			Debug.Log("RT");
        }

		if (state.Triggers.Left != 0) {
			Debug.Log("LT");
        }
        
        
        /*if(Input.GetAxis ("Lexitron Stick 1 Horizontal") != 0) {
			Debug.Log ("Horizontal: " + Input.GetAxis ("Lexitron Stick 1 Horizontal"));
		}

		if(Input.GetAxis ("Lexitron Stick 1 Vertical") != 0) {
			Debug.Log ("Vertical: " + Input.GetAxis ("Lexitron Stick 1 Vertical"));
		}

		if(Input.GetKey(KeyCode.Joystick1Button0)) {
			Debug.Log("A");
		}

		if(Input.GetKey(KeyCode.Joystick1Button1)) {
			Debug.Log("B");
		}

		if(Input.GetKey(KeyCode.Joystick1Button2)) {
			Debug.Log("X");
		}

		if(Input.GetKey(KeyCode.Joystick1Button3)) {
			Debug.Log("Y");
		}*/

	}
}
