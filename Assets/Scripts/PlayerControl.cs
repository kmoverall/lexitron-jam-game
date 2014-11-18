using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour {
	
	BeatManager beatz;
	LevelReader level;
    Score score;

    public Transform laserParticle;
    public Transform wormholeParticle;

	public Transform topLane;
	public Transform midLane;
	public Transform botLane;

	public enum PlayerState {CRUISING, MOVING};
	public enum Lanes {TOP, MID, BOT};
	public enum SideLanes {LEFT, MID, RIGHT};
	private PlayerState state;
	public SideLanes sidepos;
	public  Lanes pos;
	private Vector3 moveTarget;


	private Vector3 neutralPos;

	public float speed;

	GamePadState inputState;
    GamePadState prevState;
	PlayerIndex playerIndex;

	// Use this for initialization
	void Start () {
		beatz = GameObject.FindObjectOfType<Camera>().GetComponent<BeatManager>();
		level = GameObject.FindObjectOfType<Camera>().GetComponent<LevelReader>();
        score = GameObject.FindObjectOfType<Camera>().GetComponent<Score>();

		state = PlayerState.CRUISING;
		pos = Lanes.MID;
		neutralPos = transform.position;
		moveTarget = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		inputState = GamePad.GetState(0);


		if (level.state == LevelReader.LevelState.CRUISING) {
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


		} else if (level.state == LevelReader.LevelState.WORMHOLE) {
			switch (state) {
			case PlayerState.CRUISING:
				if (inputState.DPad.Up == ButtonState.Pressed) {
					pos = Lanes.TOP;
					moveTarget.y = topLane.position.y;
				} else if (inputState.DPad.Down == ButtonState.Pressed) {
					pos = Lanes.BOT;
					moveTarget.y = botLane.position.y;
				} else {
					pos = Lanes.MID;
					moveTarget.y = neutralPos.y;
				}

				if (inputState.DPad.Left == ButtonState.Pressed) {
					sidepos = SideLanes.LEFT;
					moveTarget.z = topLane.position.y;
				} else if (inputState.DPad.Right == ButtonState.Pressed) {
					sidepos = SideLanes.RIGHT;
					moveTarget.z = botLane.position.y;
				} else {
					sidepos = SideLanes.MID;
					moveTarget.z = neutralPos.z;
				}

				if (transform.position != moveTarget) {
					state = PlayerState.MOVING;
				}
                break;


            case PlayerState.MOVING:
				Move();
				if (gameObject.transform.position.y == moveTarget.y && gameObject.transform.position.x == moveTarget.x) {
					state = PlayerState.CRUISING;
                }
				break;
            }


		} else if (level.state == LevelReader.LevelState.ENTERING) {
			switch (state) {
			case PlayerState.CRUISING:
				if (pos != Lanes.MID) {
					pos = Lanes.MID;
					moveTarget = neutralPos;
					state = PlayerState.MOVING;
				}
				break;
			case PlayerState.MOVING:
				Move();
				if (gameObject.transform.position.y == moveTarget.y && gameObject.transform.position.x == moveTarget.x) {
					state = PlayerState.CRUISING;
                }
                break;
            }


		} else if (level.state == LevelReader.LevelState.EXITING) {
			switch (state) {
			case PlayerState.CRUISING:
				if (pos != Lanes.MID) {
					pos = Lanes.MID;
					moveTarget = neutralPos;
					state = PlayerState.MOVING;
				}
				break;
			case PlayerState.MOVING:
				Move();
				if (gameObject.transform.position.y == moveTarget.y && gameObject.transform.position.x == moveTarget.x) {
                    state = PlayerState.CRUISING;
                }
                break;
            }
        }

        if (ButtonSet.AnyButtonPressed(inputState, prevState) && level.state == LevelReader.LevelState.CRUISING)
        {
            Vector3 laserPos = transform.position;
            laserPos.x += transform.localScale.x / 2;
            Instantiate(laserParticle, laserPos, Quaternion.Euler(0, 90, 0));
        }

        /*if (ButtonSet.AnyButtonPressed(inputState, prevState) && level.state == LevelReader.LevelState.WORMHOLE)
        {
            Vector3 laserPos = transform.position;
            laserPos.x += transform.localScale.x / 2;
            Instantiate(wormholeParticle, laserPos, Quaternion.Euler(0, 90, 0));
        }*/

        prevState = inputState;
    }

    
    void Move() {
        float step = (float)speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, moveTarget, step);
	}
}
