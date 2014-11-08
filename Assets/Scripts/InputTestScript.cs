using UnityEngine;
using System.Collections;

public class InputTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxis ("Lexitron Stick 1 Horizontal") != 0) {
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
		}

	}
}
