﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class ButtonSet {
	public enum TestButtons {A, B, X, Y, RB, LB};
	public enum TestDirections {Up, Down, Left, Right}

	public Dictionary<TestButtons, ButtonState> buttonState;
	public Dictionary<TestDirections, ButtonState> stickState;

	public ButtonSet() {
		buttonState = new Dictionary<TestButtons, ButtonState>();
		stickState = new Dictionary<TestDirections, ButtonState>();

		buttonState.Add(TestButtons.A, ButtonState.Released);
		buttonState.Add(TestButtons.B, ButtonState.Released);
		buttonState.Add(TestButtons.X, ButtonState.Released);
		buttonState.Add(TestButtons.Y, ButtonState.Released);
		buttonState.Add(TestButtons.RB, ButtonState.Released);
		buttonState.Add(TestButtons.LB, ButtonState.Released);
		stickState.Add(TestDirections.Up, ButtonState.Released);
		stickState.Add(TestDirections.Down, ButtonState.Released);
		stickState.Add(TestDirections.Left, ButtonState.Released);
		stickState.Add(TestDirections.Right, ButtonState.Released);
	}

	public bool buttonHoldCompare(GamePadState state) {
		if (state.Buttons.A != buttonState[TestButtons.A]) {
			return false;
		}
		if (state.Buttons.B != buttonState[TestButtons.B]) {
			return false;
		}
		if (state.Buttons.X != buttonState[TestButtons.X]) {
			return false;
		}
		if (state.Buttons.Y != buttonState[TestButtons.Y]) {
			return false;
		}
		if (state.Buttons.RightShoulder != buttonState[TestButtons.RB]) {
			return false;
		}
		if (state.Buttons.LeftShoulder != buttonState[TestButtons.LB]) {
			return false;
		}
		return true;
	}

	public bool directionHoldCompare(GamePadState state) {
		if (state.DPad.Up != stickState[TestDirections.Up]) {
			return false;
		}
		if (state.DPad.Down != stickState[TestDirections.Down]) {
			return false;
		}
		if (state.DPad.Right != stickState[TestDirections.Right]) {
			return false;
		}
		if (state.DPad.Left != stickState[TestDirections.Left]) {
			return false;
		}
		return true;
	}

	public bool buttonPressCompare(GamePadState state, GamePadState prev) {
		if (state.Buttons.A == ButtonState.Pressed && prev.Buttons.A == ButtonState.Released 
		    && buttonState[TestButtons.A] == ButtonState.Pressed) {
			return true;
		}
		if (state.Buttons.B == ButtonState.Pressed && prev.Buttons.B == ButtonState.Released 
		    && buttonState[TestButtons.B] == ButtonState.Pressed) {
			return true;
		}
		if (state.Buttons.X == ButtonState.Pressed && prev.Buttons.X == ButtonState.Released 
		    && buttonState[TestButtons.X] == ButtonState.Pressed) {
			return true;
		}
		if (state.Buttons.Y == ButtonState.Pressed && prev.Buttons.Y == ButtonState.Released 
		    && buttonState[TestButtons.Y] == ButtonState.Pressed) {
			return true;
		}
		if (state.Buttons.RightShoulder == ButtonState.Pressed && prev.Buttons.RightShoulder == ButtonState.Released 
		    && buttonState[TestButtons.RB] == ButtonState.Pressed) {
			return true;
		}
		if (state.Buttons.LeftShoulder == ButtonState.Pressed && prev.Buttons.LeftShoulder == ButtonState.Released 
		    && buttonState[TestButtons.LB] == ButtonState.Pressed) {
			return true;
		}
		return false;
	}
	
	public bool directionPressCompare(GamePadState state, GamePadState prev) {
		if (state.DPad.Up == ButtonState.Pressed && prev.DPad.Up == ButtonState.Released 
		    && stickState[TestDirections.Up] == ButtonState.Pressed) {
			return true;
		}
		if (state.DPad.Down == ButtonState.Pressed && prev.DPad.Down == ButtonState.Released 
		    && stickState[TestDirections.Down] == ButtonState.Pressed) {
			return true;
		}
		if (state.DPad.Left == ButtonState.Pressed && prev.DPad.Left == ButtonState.Released 
		    && stickState[TestDirections.Left] == ButtonState.Pressed) {
			return true;
		}
		if (state.DPad.Right == ButtonState.Pressed && prev.DPad.Right == ButtonState.Released 
		    && stickState[TestDirections.Right] == ButtonState.Pressed) {
			return true;
		}
		return false;
	}
}