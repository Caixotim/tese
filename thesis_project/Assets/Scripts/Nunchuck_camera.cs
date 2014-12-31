using UnityEngine;
using System.Collections;

public class Nunchuck_camera : MonoBehaviour {

	private WiimoteReceiver receiver;
//	public Camera mainCamera;
//	private float PlayerMovementSpeed = 10;

	// Use this for initialization
	void Start () {
		receiver = new WiimoteReceiver();
		receiver.connect();

	}

	// Update is called once per frame
	void Update () {
		//overwrite the movement on the oculusrift script that has the 360 controller
//		if(receiver.wiimotes.ContainsKey(1))
//		{
//			NunchuckControls();
//		}

	}

//	private void NunchuckControls()
//	{
//		Wiimote mymote = receiver.wiimotes[1];
//
//		float delta_y = mymote.NUNCHUK_JOY_Y - 0.52f;
//		float delta_x = mymote.NUNCHUK_JOY_Y - 0.52f;
//		if (Mathf.Abs (delta_y) < 0.02f)
//			delta_y = 0;
//		if (Mathf.Abs (delta_x) < 0.02f)
//			delta_x = 0;
//
//		//Control Movement
//		mainCamera.transform.Translate (0, 0, delta_y * Time.deltaTime * PlayerMovementSpeed);
//		mainCamera.transform.Translate(delta_x * Time.deltaTime * PlayerMovementSpeed,0,0);
//
//	}


}
