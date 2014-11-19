using UnityEngine;
using System.Collections;
using XInputDotNetPure;

//This is a boilerplate function to provide a simple interface into XInput

public class ArcadeManager : MonoBehaviour {

    public enum Buttons { A, B, X, Y, LB, RB, LT, RT, Up, Down, Left, Right };

    GamePadState inputState;
    GamePadState prevState;

	// Use this for initialization
	void Start () {
        inputState = GamePad.GetState(0);
        prevState = GamePad.GetState(0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        prevState = inputState;
	}

    //Checks if the given button was pressed in the past frame
    public bool IsButtonPressed(Buttons button)
    {
        inputState = GamePad.GetState(0);

        switch (button)
        {
            case Buttons.A:
                return (inputState.Buttons.A == ButtonState.Pressed && prevState.Buttons.A != ButtonState.Pressed);
            case Buttons.B:
                return (inputState.Buttons.B == ButtonState.Pressed && prevState.Buttons.B != ButtonState.Pressed);
            case Buttons.X:
                return (inputState.Buttons.X == ButtonState.Pressed && prevState.Buttons.X != ButtonState.Pressed);
            case Buttons.Y:
                return (inputState.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y != ButtonState.Pressed);
            case Buttons.LB:
                return (inputState.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder != ButtonState.Pressed);
            case Buttons.RB:
                return (inputState.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder != ButtonState.Pressed);
            case Buttons.LT:
                return (inputState.Triggers.Left != 0 && prevState.Triggers.Left == 0);
            case Buttons.RT:
                return (inputState.Triggers.Right != 0 && prevState.Triggers.Right == 0);
            case Buttons.Up:
                return (inputState.DPad.Up == ButtonState.Pressed && prevState.DPad.Up != ButtonState.Pressed);
            case Buttons.Left:
                return (inputState.DPad.Left == ButtonState.Pressed && prevState.DPad.Left != ButtonState.Pressed);
            case Buttons.Down:
                return (inputState.DPad.Down == ButtonState.Pressed && prevState.DPad.Down != ButtonState.Pressed);
            case Buttons.Right:
                return (inputState.DPad.Right == ButtonState.Pressed && prevState.DPad.Right != ButtonState.Pressed);
            default:
                return false;
        }
    }

    //Checks if the given button is down on this frame
    public bool IsButtonHeld(Buttons button)
    {
        inputState = GamePad.GetState(0);

        switch (button)
        {
            case Buttons.A:
                return (inputState.Buttons.A == ButtonState.Pressed);
            case Buttons.B:
                return (inputState.Buttons.B == ButtonState.Pressed);
            case Buttons.X:
                return (inputState.Buttons.X == ButtonState.Pressed);
            case Buttons.Y:
                return (inputState.Buttons.Y == ButtonState.Pressed);
            case Buttons.LB:
                return (inputState.Buttons.LeftShoulder == ButtonState.Pressed);
            case Buttons.RB:
                return (inputState.Buttons.RightShoulder == ButtonState.Pressed);
            case Buttons.LT:
                return (inputState.Triggers.Left != 0);
            case Buttons.RT:
                return (inputState.Triggers.Right != 0);
            case Buttons.Up:
                return (inputState.DPad.Up == ButtonState.Pressed);
            case Buttons.Left:
                return (inputState.DPad.Left == ButtonState.Pressed);
            case Buttons.Down:
                return (inputState.DPad.Down == ButtonState.Pressed);
            case Buttons.Right:
                return (inputState.DPad.Right == ButtonState.Pressed);
            default:
                return false;
        }
    }

    public bool AnyButtonPressed()
    {
        return ((inputState.Buttons.A == ButtonState.Pressed && prevState.Buttons.A != ButtonState.Pressed) ||
            (inputState.Buttons.B == ButtonState.Pressed && prevState.Buttons.B != ButtonState.Pressed) ||
            (inputState.Buttons.X == ButtonState.Pressed && prevState.Buttons.X != ButtonState.Pressed) ||
            (inputState.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y != ButtonState.Pressed) ||
            (inputState.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder != ButtonState.Pressed) ||
            (inputState.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder != ButtonState.Pressed));
    }
}
