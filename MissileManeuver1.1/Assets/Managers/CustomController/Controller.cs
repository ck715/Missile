using UnityEngine;
using System.Collections;

namespace CustomController {
	public class Controller {

		private const float _tapTime = 0.5f;
		private const float _holdTime = 1.0f;

		private const float _joystickTapAmt = 0.75f;

		private static float _left, _right, _up, _down, _a, _b, _x, _y, _start, _select, _l1, _l2, _r1, _r2, _l3, _r3, _dh, _dv, _ljh, _ljv, _rjh, _rjv;


		// Did the player tap the button?
		public static bool Tap(controllerButton button, float tapTime = _tapTime){
			//if(Input.GetAxis(inputString(button)) == 0)
			//	return false;

			float time = getTime (button);
			checkInput (button);
			return time == -1 && getTime (button) != -1;

//			return time >= 0 && time < tapTime;
		}

		// Has the button been held down long enough?
		public static bool Hold(controllerButton button, float holdTime = _holdTime){
			//if(Input.GetAxis(inputString(button)) == 0)
			//	return false;
			
			checkInput (button);
			float time = getTime (button);
			
			return time > 0 && time > holdTime;
		}

		// Is the button held down?
		public static bool Down(controllerButton button){
			checkInput (button);
			return getTime(button) != -1;
		}

		public static bool Up(controllerButton button){
			float time = getTime (button);
			checkInput (button);

			return time != -1 && getTime (button) == -1;
		}

		// Helper functions
		private static void checkInput(controllerButton button){
			float value;

			// Left [Left Joystick + D-Pad)
			if (button == controllerButton.Left) {
				value = Mathf.Max(Input.GetAxis (inputString (controllerButton.LHorizontal)), Input.GetAxis (inputString (controllerButton.DHorizontal)), Input.GetKey(KeyCode.LeftArrow)?1:0);
				
				_left = value <= _joystickTapAmt ? -1 : value;
			}
			// Right [Left Joystick + D-Pad)
			else if (button == controllerButton.Right) {
				value = Mathf.Max(-1 * Mathf.Min(Input.GetAxis (inputString (controllerButton.LHorizontal)), Input.GetAxis (inputString (controllerButton.DHorizontal))), Input.GetKey(KeyCode.RightArrow) ? 1 : 0);
				
				_right = value <= _joystickTapAmt ? -1 : value;
			}
			// Up [Left Joystick + D-Pad)
			else if (button == controllerButton.Up) {
				value = Mathf.Max(Input.GetAxis (inputString (controllerButton.LVertical)), Input.GetAxis (inputString (controllerButton.DVertical)), Input.GetKey(KeyCode.UpArrow)?1:0);
				
				_up = value <= _joystickTapAmt ? -1 : value;

				//Debug.Log("Up: " + _up);
			}
			// Down [Left Joystick + D-Pad)
			else if (button == controllerButton.Down) {
				value = Mathf.Max(-1* Mathf.Min(Input.GetAxis (inputString (controllerButton.LVertical)), Input.GetAxis (inputString (controllerButton.DVertical))), Input.GetKey(KeyCode.DownArrow) ? 1 : 0);
				
				_down = value <= _joystickTapAmt ? -1 : value;

				//Debug.Log("Down: " + _down);
			}

			else{
				value = Input.GetAxis (inputString (button));

				// single values
				if (button == controllerButton.A)
					_a = value == 0 ? -1 : value;
				else if (button == controllerButton.B)
					_b = value == 0 ? -1 : value;
				else if (button == controllerButton.X)
					_x = value == 0 ? -1 : value;
				else if (button == controllerButton.Y)
					_y = value == 0 ? -1 : value;
				else if (button == controllerButton.Start)
					_start = value == 0 ? -1 : value;
				else if (button == controllerButton.Select)
					_select = value == 0 ? -1 : value;
				else if (button == controllerButton.L1)
					_l1 = value == 0 ? -1 : value;
				else if (button == controllerButton.L2)
					_l2 = value == 0 ? -1 : value;
				else if (button == controllerButton.L3)
					_l3 = value == 0 ? -1 : value;
				else if (button == controllerButton.R1)
					_r1 = value == 0 ? -1 : value;
				else if (button == controllerButton.R2)
					_r2 = value == 0 ? -1 : value;
				else if (button == controllerButton.R3)
					_r3 = value == 0 ? -1 : value;

				// D-Pad only
				else if (button == controllerButton.DHorizontal)
					_dh = value == 0 ? -1 : value;
				else if (button == controllerButton.DVertical)
					_dv = value == 0 ? -1 : value;

				// Left Joystick only
				else if (button == controllerButton.LHorizontal)
					_ljh = value == 0 ? -1 : value;
				else if (button == controllerButton.LVertical)
					_ljv = value == 0 ? -1 : value;

				// Right Joystick only
				else if (button == controllerButton.RHorizontal)
					_rjh = value == 0 ? -1 : value;
				else if (button == controllerButton.RVertical)
					_rjv = value == 0 ? -1 : value;
			}

		}

		private static float getTime(controllerButton button){
			if(getValue(button) == -1)
				return -1;

			return Time.time - getValue (button);
		}

		private static float getValue(controllerButton button){
			// single values
			if (button == controllerButton.A)
				return _a;
			else if (button == controllerButton.B)
				return _b;
			else if (button == controllerButton.X)
				return _x;
			else if (button == controllerButton.Y)
				return _y;
			else if (button == controllerButton.Start)
				return _start;
			else if (button == controllerButton.Select)
				return _select;
			else if (button == controllerButton.L1)
				return _l1;
			else if (button == controllerButton.L2)
				return _l2;
			else if (button == controllerButton.L3)
				return _l3;
			else if (button == controllerButton.R1)
				return _r1;
			else if (button == controllerButton.R2)
				return _r2;
			else if (button == controllerButton.R3)
				return _r3;
			
			// D-Pad only
			else if (button == controllerButton.DHorizontal)
				return _dh;
			else if (button == controllerButton.DVertical)
				return _dv;
			
			// Left Joystick only
			else if (button == controllerButton.LHorizontal)
				return _ljh;
			else if (button == controllerButton.LVertical)
				return _ljv;
			
			// Right Joystick only
			else if (button == controllerButton.RHorizontal)
				return _rjh;
			else if (button == controllerButton.RVertical)
				return _rjv;
			
			// Left [Left Joystick + D-Pad)
			else if (button == controllerButton.Left) {
				return _left;
			}
			// Right [Left Joystick + D-Pad)
			else if (button == controllerButton.Right) {
				return _right;
			}
			// Up [Left Joystick + D-Pad)
			else if (button == controllerButton.Up) {
				return _up;
			}
			// Down [Left Joystick + D-Pad)
			else if (button == controllerButton.Down) {
				return _down;
			}

			return 0;
		}

		private static string inputString(controllerButton button){
			// Windows
			if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor){
				// buttons
				if(button == controllerButton.A)
					return "joystick button 0";
				else if(button == controllerButton.B)
					return "joystick button 1";
				else if(button == controllerButton.X)
					return "joystick button 2";
				else if(button == controllerButton.Y)
					return "joystick button 3";
				else if(button == controllerButton.L1)
					return "joystick button 4";
				else if(button == controllerButton.R1)
					return "joystick button 5";
				else if(button == controllerButton.Select)
					return "joystick button 6";
				else if(button == controllerButton.Start)
					return "joystick button 7";

				// left joystick
				else if(button == controllerButton.LHorizontal)
					return "X Axis";
				else if(button == controllerButton.LVertical)
					return "Y Axis";
				else if(button == controllerButton.L3)
					return "joystick button 8";

				// bumpers
				else if(button == controllerButton.L2)
					return "9th Axis";
				else if(button == controllerButton.R2)
					return "10th Axis";

				// right joystick
				else if(button == controllerButton.RHorizontal)
					return "4th Axis";
				else if(button == controllerButton.RVertical)
					return "5th Axis";
				else if(button == controllerButton.R3)
					return "joystick button 9";

				// D-Pad
				else if(button == controllerButton.DHorizontal)
					return "6th Axis";
				else if(button == controllerButton.DVertical)
					return "7th Axis";
			}

			// OS X
			if(Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor){
				// buttons
				if(button == controllerButton.A)
					return "joystick button 16";
				else if(button == controllerButton.B)
					return "joystick button 17";
				else if(button == controllerButton.X)
					return "joystick button 18";
				else if(button == controllerButton.Y)
					return "joystick button 19";
				else if(button == controllerButton.L1)
					return "joystick button 13";
				else if(button == controllerButton.R1)
					return "joystick button 14";
				else if(button == controllerButton.Select)
					return "joystick button 10";
				else if(button == controllerButton.Start)
					return "joystick button 9";
				
				// left joystick
				else if(button == controllerButton.LHorizontal)
					return "X Axis";
				else if(button == controllerButton.LVertical)
					return "Y Axis";
				else if(button == controllerButton.L3)
					return "joystick button 11";
				
				// bumpers
				else if(button == controllerButton.L2)
					return "5th Axis";
				else if(button == controllerButton.R2)
					return "6th Axis";
				
				// right joystick
				else if(button == controllerButton.RHorizontal)
					return "3th Axis";
				else if(button == controllerButton.RVertical)
					return "4th Axis";
				else if(button == controllerButton.R3)
					return "joystick button 12";
				
				// D-Pad
				else if(button == controllerButton.DHorizontal)
					return "joystick button 5-6";
				else if(button == controllerButton.DVertical)
					return "joystick button 7-8";
			}

			// Linux
			if(Application.platform == RuntimePlatform.LinuxPlayer){
				// buttons
				if(button == controllerButton.A)
					return "joystick button 0";
				else if(button == controllerButton.B)
					return "joystick button 1";
				else if(button == controllerButton.X)
					return "joystick button 2";
				else if(button == controllerButton.Y)
					return "joystick button 3";
				else if(button == controllerButton.L1)
					return "joystick button 4";
				else if(button == controllerButton.R1)
					return "joystick button 5";
				else if(button == controllerButton.Select)
					return "joystick button 8";
				else if(button == controllerButton.Start)
					return "joystick button 9";
				
				// left joystick
				else if(button == controllerButton.LHorizontal)
					return "X Axis";
				else if(button == controllerButton.LVertical)
					return "Y Axis";
				else if(button == controllerButton.L3)
					return "joystick button 11";
				
				// bumpers
				else if(button == controllerButton.L2)
					return "joystick button 6";
				else if(button == controllerButton.R2)
					return "joystick button 7";
				
				// right joystick
				else if(button == controllerButton.RHorizontal)
					return "4th Axis";
				else if(button == controllerButton.RVertical)
					return "5th Axis";
				else if(button == controllerButton.R3)
					return "joystick button 12";
				
				// D-Pad
				else if(button == controllerButton.DHorizontal)
					return "7th Axis";
				else if(button == controllerButton.DVertical)
					return "8th Axis";
			}

			// null response
			return "";
		}
		
	}

	public enum controllerButton {
		A,
		B,
		X,
		Y,
		Start,
		Select,
		L1,
		L2,
		L3,
		R1,
		R2,
		R3,
		Left,
		Right,
		Up,
		Down,

		LHorizontal,
		LVertical,

		RHorizontal,
		RVertical,

		DHorizontal,
		DVertical
	};
}