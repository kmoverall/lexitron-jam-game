using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour {
	
	BeatManager beatz;

	public Transform topLane;
	public Transform midLane;
	public Transform botLane;

	public Transform reticle;

	public enum PlayerState {CRUISING, MOVING};
	public enum PlayerPosition {TOP, MID, BOT};
	private PlayerState state;
	private PlayerPosition pos;
	private Vector3 moveTarget;

	public float speed;

	GamePadState inputState;
	PlayerIndex playerIndex;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();

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

		state = PlayerState.CRUISING;
		pos = PlayerPosition.MID;
		moveTarget = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		inputState = GamePad.GetState(playerIndex);

		switch (state) {
		case PlayerState.CRUISING:
			if (inputState.DPad.Up == ButtonState.Pressed && pos != PlayerPosition.TOP) {
				pos = PlayerPosition.TOP;
				moveTarget.y = topLane.position.y;
				state = PlayerState.MOVING;
			} else if (inputState.DPad.Down == ButtonState.Pressed && pos != PlayerPosition.BOT) {
				pos = PlayerPosition.BOT;
				moveTarget.y = botLane.position.y;
				state = PlayerState.MOVING;
			} else if (inputState.DPad.Up != ButtonState.Pressed && inputState.DPad.Down != ButtonState.Pressed && pos != PlayerPosition.MID) {
				pos = PlayerPosition.MID;
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
