using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour {
	
	BeatManager beatz;

	public Transform topLane;
	public Transform midLane;
	public Transform botLane;

	public enum PlayerState {CRUISING, MOVING};
	public enum Lanes {TOP, MID, BOT};
	private PlayerState state;
	public  Lanes pos;
	private Vector3 moveTarget;

	public float speed;

	GamePadState inputState;
	PlayerIndex playerIndex;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();

		state = PlayerState.CRUISING;
		pos = Lanes.MID;
		moveTarget = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		inputState = GamePad.GetState(0);

		switch (state) {
		case PlayerState.CRUISING:
			if (inputState.DPad.Up == ButtonState.Pressed && pos != Lanes.TOP) {
				pos = Lanes.TOP;
				moveTarget.y = topLane.position.y;
				state = PlayerState.MOVING;
			} else if (inputState.DPad.Down == ButtonState.Pressed && pos != Lanes.BOT) {
				pos = Lanes.BOT;
				moveTarget.y = botLane.position.y;
				state = PlayerState.MOVING;
			} else if (inputState.DPad.Up != ButtonState.Pressed && inputState.DPad.Down != ButtonState.Pressed && pos != Lanes.MID) {
				pos = Lanes.MID;
				moveTarget.y = midLane.position.y;
				state = PlayerState.MOVING;
            }
            break;

		case PlayerState.MOVING:
			Move();
			if (gameObject.transform.position.y == moveTarget.y) {
				state = PlayerState.CRUISING;
			}
			break;
        }
    }

	void Move() {
		float step = (float)speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, moveTarget, step);
	}
}
