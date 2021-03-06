using System;
using System.Collections;
using UnityEngine;

public class Wiimote {
	
	private int id;
	private DateTime lastUpdate;
	
	// Wiimote Buttons:
	private float m_buttonA;
	private float m_buttonB;
	private float m_buttonLeft;
	private float m_buttonRight;
	private float m_buttonUp;
	private float m_buttonDown;
	private float m_buttonOne;
	private float m_buttonTwo;
	private float m_buttonPlus;
	private float m_buttonMinus;
	private float m_buttonHome;
	
	// Wiimote Analog:
	private float m_pryPitch;
	private float m_pryRoll;
	private float m_pryYaw;
	private float m_pryAccel;
	private float m_irX;
	private float m_irY;
	private float m_irSize;
	private float m_AccX;
	private float m_AccY;
	private float m_AccZ;
	
	//Extensions:
	// Nunchuk:
	private float m_nunchukC;
	private float m_nunchukZ;
	private float m_nunchuckPitch;
	private float m_nunchukRoll;
	private float m_nunchukYaw;
	private float m_nunchukAccel;
	private float m_nunchukJoyX;
	private float m_nunchukJoyY;
	private float m_nunchukAccX;
	private float m_nunchukAccY;
	private float m_nunchukAccZ;

	private float m_nunchukJoyXSplit;
	private float m_nunchukJoyYSplit;

	// Balance Board:
	private float m_balanceBL;
	private float m_balanceBR;
	private float m_balanceTL;
	private float m_balanceTR;
	private float m_balanceSum;
	
	// Public getters
	public float BUTTON_A {get {return m_buttonA; } }
	public float BUTTON_B {get {return m_buttonB; } }
	public float BUTTON_LEFT {get {return m_buttonLeft; } }
	public float BUTTON_RIGHT {get {return m_buttonRight; } }
	public float BUTTON_UP {get {return m_buttonUp; } }
	public float BUTTON_DOWN {get {return m_buttonDown; } }
	public float BUTTON_ONE {get {return m_buttonOne; } }
	public float BUTTON_TWO {get {return m_buttonTwo; } }
	public float BUTTON_PLUS {get {return m_buttonPlus; } }
	public float BUTTON_MINUS {get {return m_buttonMinus; } }
	public float BUTTON_HOME {get {return m_buttonHome; } }
	
	public float PRY_PITCH {get {return m_pryPitch; } }
	public float PRY_ROLL {get {return m_pryRoll; } }
	public float PRY_YAW {get {return m_pryYaw; } }
	public float PRY_ACCEL {get {return m_pryAccel; } }
	public float IR_X {get {return m_irX;}}
	public float IR_Y {get {return m_irY;}}
	public float IR_SIZE {get {return m_irSize;}}
	public float ACCX {get {return m_AccX; } }
	public float ACCY {get {return m_AccY; } }
	public float ACCZ {get {return m_AccZ; } }
	
	public float NUNCHUK_C {get {return m_nunchukC;}}
	public float NUNCHUK_Z {get {return m_nunchukZ;}}
	public float NUNCHUK_PITCH {get {return m_nunchuckPitch;}}
	public float NUNCHUK_ROLL {get {return m_nunchukRoll;}}
	public float NUNCHUK_YAW {get {return m_nunchukYaw;}}
	public float NUNCHUK_ACCEL {get {return m_nunchukAccel;}}
	public float NUNCHUK_JOY_X {get {return m_nunchukJoyX;}}
	public float NUNCHUK_JOY_Y {get {return m_nunchukJoyY;}}
	public float NUNCHUK_JOY_X_SPLIT {get {return m_nunchukJoyXSplit;}}
	public float NUNCHUK_JOY_Y_SPLIT {get {return m_nunchukJoyYSplit;}}
	public float NUNCHUK_ACCX {get {return m_nunchukAccX;}}
	public float NUNCHUK_ACCY {get {return m_nunchukAccY;}}
	public float NUNCHUK_ACCZ {get {return m_nunchukAccZ;}}
	
	public float BALANCE_BOTTOMLEFT {get {return m_balanceBL;}}
	public float BALANCE_BOTTOMRIGHT {get {return m_balanceBR;}}
	public float BALANCE_TOPLEFT {get {return m_balanceTL;}}
	public float BALANCE_TOPRIGHT {get {return m_balanceTR;}}
	public float BALANCE_SUM {get {return m_balanceSum;}}
	
	public enum KeyCode {
		BUTTON_A,
		BUTTON_B,
		BUTTON_MINUS,
		BUTTON_PLUS,
		BUTTON_ONE,
		BUTTON_TWO,
		BUTTON_UP,
		BUTTON_DOWN,
		BUTTON_LEFT,
		BUTTON_RIGHT,
		NUNCHUCK_C,
		NUNCHUCK_Z,
		NUNCHUCK_ANALOG_UP,
		NUNCHUCK_ANALOG_DOWN,
		NUNCHUCK_ANALOG_LEFT,
		NUNCHUCK_ANALOG_RIGHT,
		NONE
	}
	
// 	private ArrayList <KeyCode> activeKeys = new ArrayList<KeyCode>();
	private KeyCode currentKeyCode = KeyCode.NONE;
	
	public Wiimote() {}
	
	public Wiimote(int id)
	{
		this.id = id;
	}
	
	public void update(string oscMessage, ArrayList values, DateTime currentTime)
	{
		lastUpdate = currentTime;
		// Analog Wiimote
		switch (oscMessage)
		{
			case "accel/pry":
				m_pryPitch = (float)values[0];
				m_pryRoll = (float)values[1];
				m_pryYaw = (float)values[2];
				m_pryAccel = (float)values[3];
				break;
			case "accel/xyz":
				m_AccX = (float)values[0];
				m_AccY = (float)values[1];
				m_AccZ = (float)values[2];
				break;
			case "ir":
				m_irX = (float)values[0];
				m_irY = (float)values[1];
				break;
			case "ir/xys/1":
				m_irSize = (float)values[2];
				break;
			case "button/A":
				m_buttonA = (float)values[0];
				break;
			case "button/B":
				m_buttonB = (float)values[0];
				break;
			case "button/Left":
				m_buttonLeft = (float)values[0];
				break;
			case "button/Right":
				m_buttonRight = (float)values[0];
				break;
			case "button/Up":
				m_buttonUp = (float)values[0];
				break;
			case "button/Down":
				m_buttonDown = (float)values[0];
				break;
			case "button/Plus":
				m_buttonPlus = (float)values[0];
				break;
			case "button/Minus":
				m_buttonMinus = (float)values[0];
				break;
			case "button/1":
				m_buttonOne = (float)values[0];
				break;
			case "button/2":
				m_buttonTwo = (float)values[0];
				break;
			case "button/Home":
				m_buttonHome = (float)values[0];
				break;
			case "balance":
				m_balanceBL = (float)values[0];
				m_balanceBR = (float)values[1];
				m_balanceTL = (float)values[2];
				m_balanceTR = (float)values[3];
				m_balanceSum = (float)values[4];
				break;
			case "nunchuk/button/C":
				m_nunchukC = (float)values[0];
				break;
			case "nunchuk/button/Z":
				m_nunchukZ = (float)values[0];
				break;
			case "nunchuk/joy":
				m_nunchukJoyX = (float)values[0];
				m_nunchukJoyY = (float)values[1];
				break;
			case "nunchuk/joy/0":
				m_nunchukJoyXSplit = (float) values[0];
				break;
			case "nunchuk/joy/1":
				m_nunchukJoyYSplit = (float) values[0];
				break;
			case "nunchuk/accel/pry":
				m_nunchuckPitch = (float)values[0];
				m_nunchukRoll = (float)values[1];
				m_nunchukYaw = (float)values[2];
				m_nunchukAccel = (float)values[3];
				break;
			case "nunchuk/accel/xyz":
				m_nunchukAccX = (float)values[0];
				m_nunchukAccY = (float)values[1];
				m_nunchukAccZ = (float)values[2];
				break;
			default: 
//				Debug.LogError("Invalid message '" + oscMessage + "' from Osculator!!!");
				break;
		}
	}
	
	public bool GetKeyPress(int keyCode) {
		KeyCode newKeyCode = (KeyCode) keyCode;
		bool isActive = false; 
		
		switch (newKeyCode) {
		case KeyCode.BUTTON_A:
			Debug.Log (m_buttonA);
			if (BUTTON_A == 1.0f) {
//				Debug.Log ("pressed A");
				isActive = true;
			}
			break; 
		case KeyCode.BUTTON_B:
			if (m_buttonB == 1.0f) {
//				Debug.Log ("pressed B");
				isActive = true;
			}
			break;
		case KeyCode.BUTTON_MINUS:
			if (m_buttonMinus == 1.0f) {
//				Debug.Log ("pressed Minus");
				isActive = true;
			}
			break;
		case KeyCode.BUTTON_UP:
			if (m_buttonUp == 1.0f) {
//				Debug.Log ("pressed Up");
				isActive = true;
			}
			break;
		case KeyCode.BUTTON_DOWN:
			if (m_buttonDown == 1.0f) {
//				Debug.Log ("pressed Down");
				isActive = true;
			}
			break;
		case KeyCode.BUTTON_LEFT:
			if (m_buttonLeft == 1.0f) {
//				Debug.Log ("pressed left");
				isActive = true;
			}
			break;
		case KeyCode.BUTTON_RIGHT:
			if (m_buttonRight == 1.0f) {
//				Debug.Log ("pressed right");
				isActive = true;
			}
			break;
		case KeyCode.NUNCHUCK_ANALOG_UP:
			if (Mathf.Abs(m_nunchukJoyYSplit) > 0.5f && m_nunchukJoyYSplit > 0) {
//				Debug.Log ("pressed analog up");
				isActive = true;
			}
			break;
		case KeyCode.NUNCHUCK_ANALOG_DOWN:
			if (Mathf.Abs(m_nunchukJoyYSplit) > 0.5f && m_nunchukJoyYSplit < 0) {
//				Debug.Log ("pressed analog down");
				isActive = true;
			}
			break;
		case KeyCode.NUNCHUCK_ANALOG_LEFT:
			if (Mathf.Abs(m_nunchukJoyXSplit) > 0.5f && m_nunchukJoyXSplit < 0) {
				// Debug.Log ("pressed analog left");
				isActive = true;
			}
			break;
		case KeyCode.NUNCHUCK_ANALOG_RIGHT:
			if (Mathf.Abs(m_nunchukJoyXSplit) > 0.5f && m_nunchukJoyXSplit > 0) {
				// Debug.Log ("pressed analog right");
				isActive = true;
			}
			break;
		case KeyCode.NUNCHUCK_C:
			if (m_nunchukC == 1.0f) {
//				Debug.Log ("pressed nunchuck C");
				isActive = true;
			}
			else {
				isActive = false;
			}
			break;
		case KeyCode.NUNCHUCK_Z:
			if (m_nunchukZ == 1.0f) {
//				Debug.Log ("pressed nunchuck Z");
				isActive = true;
			}
			break;
		}

		if (newKeyCode != currentKeyCode && isActive) {
			currentKeyCode = newKeyCode;			
			return true;
		} else if (!isActive && currentKeyCode != KeyCode.NONE && newKeyCode == currentKeyCode) {
			currentKeyCode = KeyCode.NONE;
		}
	 		
		return false;
	}
}

/*

OSCulator - OSC messages. (All data between 0 and 1 float)

Classic Controller:

/wii/x/classic/joyl
	0 = x
	1 = y
/wii/x/classic/joyr
	0 = x
	1 = y
/wii/x/classic/analog/L
/wii/x/classic/analog/R

/wii/x/classic/button/L
/wii/x/classic/button/R
/wii/x/classic/button/Left
/wii/x/classic/button/Right
/wii/x/classic/button/Up
/wii/x/classic/button/Down

/wii/x/classic/button/Minus
/wii/x/classic/button/Plus
/wii/x/classic/button/Home
/wii/x/classic/button/A
/wii/x/classic/button/B
/wii/x/classic/button/X
/wii/x/classic/button/Y
/wii/x/classic/button/ZL
/wii/x/classic/button/ZR

Nunchuk:

/wii/x/nunchuk/button/C
/wii/x/nunchuk/button/Z
/wii/x/nunchuk/joy
	0 = x
	1 = y
/wii/x/nunchuk/accel/pry
	0 = pitch
	1 = roll
	2 = yaw
	3 = accel
/wii/x/nunchuk/accel/xyz
	0 = x
	1 = y
	2 = z

Wiimote:

/wii/x/accel/xyz
	0 = x
	1 = y
	2 = z

/wii/x/accel/pry
	0 = pitch
	1 = roll
	2 = yaw
	3 = accel

/wii/x/button/A
/wii/x/button/B
/wii/x/button/Left
/wii/x/button/Right
/wii/x/button/Up
/wii/x/button/Down
/wii/x/button/Minus
/wii/x/button/Plus
/wii/x/button/1
/wii/x/button/2

/wii/x/ir
	0 = x
	1 = y

Balance Board:

Button a is sent as a normal wiimote event.
/wii/x/balance
	0 = bottom left
	1 = bottom right
	2 = top left
	3 = top right
	4 = sum
*/