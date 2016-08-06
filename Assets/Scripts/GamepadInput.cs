using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class assumes only 1 controller is plugged into the machine.
/// The code is there to recognize button presses from up to 4 controllers,
/// but all controllers will trigger joystick input on the same channel.
/// </summary>
public class MovementInput {
	private static readonly string horizontalAxisName = "Horizontal";
	private static readonly string verticalAxisName = "Vertical";

	// TODO In order to receive input from 4 distinct controller joysticks, you need to setup listeners 
	// for specific controllers in project settings -> Input, and then return data from those named listeners in these GetAxis methods
	public static float GetHorizontalAxis() { 
		return Input.GetAxis(horizontalAxisName);	
	}

	public static float GetVerticalAxis() {
		return Input.GetAxis(verticalAxisName);
	}

	public static KeyCode AButtonKeyCode(int controllerNumber = 1) {
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		switch(controllerNumber){
		case 1:
			return KeyCode.Joystick1Button16;
		case 2:
			return KeyCode.Joystick2Button16;
		case 3:
			return KeyCode.Joystick3Button16;
		case 4:
			return KeyCode.Joystick4Button16;
		default:
			break;
		}
		#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
		switch(controllerNumber){
		case 1:
		return KeyCode.Joystick1Button0;
		case 2:
		return KeyCode.Joystick2Button0;
		case 3:
		return KeyCode.Joystick3Button0;
		case 4:
		return KeyCode.Joystick4Button0;
		default:
		break;
		}
		#endif
		Debug.LogError("Unknown controller button A attempted access. Controller number: "+controllerNumber);
		return KeyCode.Z;
	}

	public static KeyCode BButtonKeyCode(int controllerNumber = 1) {
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		switch(controllerNumber){
		case 1:
			return KeyCode.Joystick1Button17;
		case 2:
			return KeyCode.Joystick2Button17;
		case 3:
			return KeyCode.Joystick3Button17;
		case 4:
			return KeyCode.Joystick4Button17;
		default:
			break;
		}
		#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
		switch(controllerNumber){
		case 1:
		return KeyCode.Joystick1Button1;
		case 2:
		return KeyCode.Joystick2Button1;
		case 3:
		return KeyCode.Joystick3Button1;
		case 4:
		return KeyCode.Joystick4Button1;
		default:
		break;
		}
		#endif
		Debug.LogError("Unknown controller button B attempted access. Controller number: "+controllerNumber);
		return KeyCode.Z;
	}

	public static KeyCode XButtonKeyCode(int controllerNumber = 1) {
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		switch(controllerNumber){
		case 1:
			return KeyCode.Joystick1Button18;
		case 2:
			return KeyCode.Joystick2Button18;
		case 3:
			return KeyCode.Joystick3Button18;
		case 4:
			return KeyCode.Joystick4Button18;
		default:
			break;
		}
		#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
		switch(controllerNumber){
		case 1:
		return KeyCode.Joystick1Button2;
		case 2:
		return KeyCode.Joystick2Button2;
		case 3:
		return KeyCode.Joystick3Button2;
		case 4:
		return KeyCode.Joystick4Button2;
		default:
		break;
		}
		#endif
		Debug.LogError("Unknown controller button X attempted access. Controller number: "+controllerNumber);
		return KeyCode.Z;
	}

	public static KeyCode YButtonKeyCode(int controllerNumber = 1) {
		#if UNITY_EDITOR || UNITY_STANDALONE_OSX
		switch(controllerNumber){
		case 1:
			return KeyCode.Joystick1Button19;
		case 2:
			return KeyCode.Joystick2Button19;
		case 3:
			return KeyCode.Joystick3Button19;
		case 4:
			return KeyCode.Joystick4Button19;
		default:
			Debug.LogError("Unknown controller button Y attempted access. Controller number: "+controllerNumber);
			break;
		}
		#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
		switch(controllerNumber){
		case 1:
		return KeyCode.Joystick1Button3;
		case 2:
		return KeyCode.Joystick2Button3;
		case 3:
		return KeyCode.Joystick3Button3;
		case 4:
		return KeyCode.Joystick4Button3;
		default:
		break;
		}
		#endif
		return KeyCode.Z;
	}
}


