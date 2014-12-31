using UnityEngine;
using System.Collections;

public class WiimoteTest : MonoBehaviour {

	private WiimoteReceiver receiver;
	public Transform mainCamera;
	private float PlayerMovementSpeed = 2;
	private float PlayerRotationSpeed = 100;
//	public Transform ControlledObj;
	public Texture cursorOne;
//	private LineRenderer lr;
	public Transform portalGun;
	private bool toPresentMessage = false;
//	private string[] Message = { "Grab","Release"};
	private string presentMessage;
	private Transform grabbedObj;
	private string objectFrontName;
	private Transform DirXform;
//	private TextMesh message3D;
	private EditorGameManager egm;
	private float grabbedObject_distance;

	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		receiver = new WiimoteReceiver();
		receiver.connect();
//		lr = GetComponent<LineRenderer>();
//		portalGun.forward = Vector3.zero;
		portalGun.forward = Vector3.Normalize(Vector3.forward);

		DirXform = GameObject.Find ("OVRPlayerController").transform;
//		message3D = GameObject.Find ("3DMessage").GetComponent<TextMesh> ();
//		Debug.Log (message3D.name);

//		Debug.Log (receiver.wiimotes.Count);

//		SetWiiMote ();

		egm.DisableMenus = false;
		
		while(true)
		{
			if(SetWiiMote ())
				return;
		}

//		MouseAimTest ();
//			Debug.Log("NO WIIMOTE DETECTED!!!");

		grabbedObject_distance = 15.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(egm.CurrentWiimote != null)
		{
			//Debug.Log("Controller found");
//			Debug.Log(receiver.wiimotes[1].IR_SIZE);
			WiiControls();
			NunchuckControls();
		}
//		else
//			Debug.Log("No controller found!!!");


		//MOUSE TEST
//		MouseAimTest();
	}

	private void WiiControls(){

		if (egm.CurrentWiimote.BUTTON_B == 1.0f) {
//			lr.useWorldSpace = false;
//			lr.SetVertexCount(2);
//			lr.SetColors(Color.red, Color.red);
//
//			RaycastHit hit;
//			if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(mymote.IR_X * Screen.width, (1 - mymote.IR_Y) * Screen.height, 0)), out hit, Mathf.Infinity))
//			{
//				lr.SetPosition(1, hit.transform.localPosition);
//			}
		}
		else
			if(egm.CurrentWiimote.BUTTON_B == 0.0f)
	        {
//				lr.enabled = false;
			}


//		float delta_x = mymote.PRY_YAW;
//		float delta_y = mymote.PRY_PITCH;
//		float delta_z = 0;
//		float? delta_x_prev = null;
//		float? delta_y_prev = null;
//
//		if (Mathf.Abs (delta_x - 0.5f) < 0.3f || (delta_x_prev != null && delta_x_prev == delta_x))
//			delta_x = 0;
//		else
//			delta_x -= 0.5f;
//		if (Mathf.Abs (delta_y - 0.515f) < 0.3f || (delta_y_prev != null && delta_y_prev == delta_y))
//			delta_y = 0;
//		else
//			delta_y -= 0.5f;
//
//		//Control Interaction
//		ControlledObj.rigidbody.AddForce(new Vector3(delta_x*15, delta_y*50, delta_z), ForceMode.Force);
//		delta_x_prev = delta_x;
//		delta_y_prev = delta_y;

		//Controll gun orientation by IR sensor
//		Vector3 crosshairPos = Camera.allCameras[0].ScreenToWorldPoint(new Vector3(egm.CurrentWiimote.IR_X * Screen.width, egm.CurrentWiimote.IR_Y * Screen.height, 5.0f));

//		Debug.Log (crosshairPos);

//		portalGun.LookAt(crosshairPos);
		float x = egm.CurrentWiimote.PRY_YAW;
		float y = egm.CurrentWiimote.PRY_PITCH;
		float z = egm.CurrentWiimote.PRY_ROLL;
//		portalGun.transform.eulerAngles = new Vector3 (x, y, z);
		portalGun.forward = new Vector3 (x, y, z);
		Debug.Log ("Direction vector: "+portalGun.forward);

		//apply raycast
		RaycastHit hit;
		Ray ray = new Ray ();
		ray.origin = portalGun.position;
		ray.direction = portalGun.forward;
		if(Physics.Raycast(ray, out hit, 50.0f))
		{
			//			Vector3 targetDir = hit.transform.position - portalGun.position;
			//			float angleBetween = Vector3.Angle(portalGun.forward, targetDir);
			//			Debug.DrawLine(portalGun.position, hit.point);
			//			Debug.Log(hit.transform.name);
//			toPresentMessage = true;
//			presentMessage = Message[0];
//			if(hit.transform.tag.Equals("movable"))
//			{
//				message3D.text = Message[0] + " " + hit.transform.name;
//				message3D.transform.position = new Vector3(hit.point.x, hit.point.y, 10.0f + ( hit.point.z) + (message3D.fontSize/1.5f));
//				message3D.transform.forward = hit.transform.forward;
//			}
//			objectFrontName = hit.transform.name;
			if(egm.CurrentWiimote.BUTTON_A == 1.0f && grabbedObj == null && hit.transform.tag.Equals("movable"))
			{
//				message3D.text="";
//				message3D.renderer.enabled = false;
//				presentMessage = Message[1];
				grabbedObj = hit.transform;
				grabbedObj.rigidbody.useGravity=false;
				grabbedObj.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
				Debug.Log("Grabbed: "+grabbedObj.name);

				grabbedObject_distance = Mathf.Abs(Vector3.Distance(hit.point, portalGun.position));

				egm.DisableMenus = true;
			}
			else if(egm.CurrentWiimote.BUTTON_A == 1.0f && grabbedObj != null)
			{
				Debug.Log("Released: "+grabbedObj.name);
				grabbedObj.rigidbody.useGravity=true;
				grabbedObj.rigidbody.constraints = RigidbodyConstraints.None;
				grabbedObj = null;
				egm.DisableMenus = false;
			}
			else if(grabbedObj != null)
			{
				Vector3 normalized_gun_vector = Vector3.Normalize(portalGun.forward);

				float Nx = portalGun.transform.position.x + (normalized_gun_vector.x*grabbedObject_distance);
				float Ny = portalGun.transform.position.y + (normalized_gun_vector.y*grabbedObject_distance);
				float Nz = portalGun.transform.position.z + (normalized_gun_vector.z*grabbedObject_distance);
				
				grabbedObj.position = new Vector3(Nx,Ny,Nz);
			}
			//			Debug.Log(portalGun.transform.forward);
		}
		//		Debug.Log(gun_dir);
//		else 
//		{
//			//			Debug.Log("");
//			message3D.text="";
////			toPresentMessage = false;
//		}
	}

	private void NunchuckControls()
	{
		Nunchuck_analogs ();
		Nunchuck_buttons ();

	}

	void OnGUI()
	{
//		Vector3 mouse_world_pos = Input.mousePosition;//Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		GUI.DrawTexture(new Rect(mouse_world_pos.x-16, Screen.height-mouse_world_pos.y-16 ,32,32), cursorOne);

		if(egm.CurrentWiimote != null)
		{	
			RenderWiimoteKeyStats();
			
//			GUI.DrawTexture(new Rect(mymote.IR_X * Screen.width/2, (1-mymote.IR_Y)* Screen.height,32,32), cursorOne);
//			GUI.DrawTexture(new Rect(mymote.IR_X * Screen.width/2 + Screen.width/2 - Screen.width/10, (1-mymote.IR_Y)* Screen.height,32,32), cursorOne);

//			if(toPresentMessage)
//			{
//				if(presentMessage.Equals(Message[0]))
//				{
//					GUI.Label(new Rect(Screen.width/2, Screen.height - Screen.height/4, 200, 20), presentMessage+": "+objectFrontName);
//				}
//				else
//					GUI.Label(new Rect(Screen.width/2, Screen.height - Screen.height/4, 200, 20), presentMessage);
//			}

//			if(egm.ActivateSelectionMenu)
//			{
////				RenderModelsMenu();
//			}
		}
	}

	private void MouseAimTest()
	{

		Vector3 mousePos = Input.mousePosition; mousePos.z = 0.5f;//mousePos.z = -(portalGun.position.x - Camera.mainCamera.transform.position.x);
//		mousePos.y = Screen.height - mousePos.y;



//		Vector3 mousePos = portalGun.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

//		Debug.Log (mousePos);

		//restrict gun angle


		Vector3 worldPos = Camera.allCameras[0].ScreenToWorldPoint(mousePos);

//		Debug.Log (worldPos);

		//restrict gun angle


//		Debug.Log (worldPos);

		portalGun.LookAt(worldPos);

//		float rotationX=0;
//		float rotationY = 0;
//		rotationX = Mathf.Clamp (portalGun.rotation.eulerAngles.x, -100, 100);
//		rotationY = Mathf.Clamp (portalGun.rotation.eulerAngles.y, -100, 100);

		
//		portalGun.eulerAngles = new Vector3(rotationX, rotationY, transform.localEulerAngles.z);

//		Debug.Log (portalGun.eulerAngles);

//		float grabbedObject_distance = 15.0f;

//		Vector3 mouse_world_pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
////		Vector3 camera_offset_calc = mainCamera.transform.position- new Vector3(mouse_world_pos.x, Screen.height-mouse_world_pos.y, 0);
//
//		portalGun.transform.LookAt (mouse_world_pos);

		RaycastHit hit;
		Ray ray = new Ray ();
		ray.origin = portalGun.position;
		ray.direction = portalGun.forward;
		if(Physics.Raycast(ray, out hit, 50.0f))
		{
//			Vector3 targetDir = hit.transform.position - portalGun.position;
//			float angleBetween = Vector3.Angle(portalGun.forward, targetDir);
//			Debug.DrawLine(portalGun.position, hit.point);
//			Debug.Log(hit.transform.name);
//			toPresentMessage = true;
//			presentMessage = Message[0];
//			if(hit.transform.tag.Equals("movable") && grabbedObj == null)
//			{
//				message3D.text = Message[0] + " " + hit.transform.name;
//				message3D.transform.position = new Vector3(hit.point.x, hit.point.y, 10.0f + ( hit.point.z) + (message3D.fontSize/1.5f));
//				message3D.transform.forward = Camera.allCameras[0].transform.forward;
//				message3D.renderer.enabled = true;
////				Debug.Log("present message");
//
//			}
//			else
//			{
//				if(message3D != null && message3D.renderer != null)
//					message3D.renderer.enabled = false;
//			}

			if(Input.GetMouseButtonDown(0) && grabbedObj == null && hit.transform.tag.Equals("movable"))
			{
//				message3D.text = "";
//				message3D.renderer.enabled = false;
				grabbedObj = hit.transform;
				grabbedObj.rigidbody.useGravity=false;
				grabbedObj.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

				grabbedObject_distance = Mathf.Abs(Vector3.Distance(hit.point, portalGun.position));//Mathf.Abs(Mathf.Sqrt(Mathf.Pow(grabbedObj.position.x-portalGun.position.x, 2) + Mathf.Pow(grabbedObj.position.y-portalGun.position.y, 2) + Mathf.Pow(grabbedObj.position.z-portalGun.position.z, 2)));

				Debug.Log("Distance: "+grabbedObject_distance);

//				Debug.Log("Grabbed: "+grabbedObj.name);
//				presentMessage = Message[1];
			}
			else if(Input.GetMouseButtonDown(0) && grabbedObj != null)
			{
				Debug.Log("Released: "+grabbedObj.name);
				grabbedObj.rigidbody.useGravity=true;
				grabbedObj.rigidbody.constraints = RigidbodyConstraints.None;
				grabbedObj = null;
			}
			else if(grabbedObj != null)
			{
				Vector3 normalized_gun_vector = Vector3.Normalize(portalGun.forward);
//				grabbedObject_distance = ;
				float Nx = Camera.allCameras[0].transform.position.x + (normalized_gun_vector.x*grabbedObject_distance);
				float Ny = Camera.allCameras[0].transform.position.y + (normalized_gun_vector.y*grabbedObject_distance);
				float Nz = Camera.allCameras[0].transform.position.z + (normalized_gun_vector.z*grabbedObject_distance);
				
				grabbedObj.position = new Vector3(Nx,Ny,Nz);
				
//				Debug.Log("obj distance: "+grabbedObject_distance);
			}
//			Debug.Log(portalGun.transform.forward);
		}
//		Debug.Log(gun_dir);
//		else 
//		{
//			if(message3D != null)
//			{
//				message3D.text = "";
//				message3D.renderer.enabled = false;
//	//			Debug.Log("");
//	//			toPresentMessage = false;
//			}
//		}



//		GUI.DrawTexture(new Rect(mymote.IR_X * Screen.width/2 + Screen.width/2 - Screen.width/10, (1-mymote.IR_Y)* Screen.height,32,32), cursorOne);
	}

	//Set Wiimote on EditorGameManager
	public bool SetWiiMote()
	{
		if(receiver.wiimotes.ContainsKey(1))
		{
			egm.CurrentWiimote = receiver.wiimotes[1];
			return true;
		}
		return false;
	}

	//Process nunchuck analog controls
	private void Nunchuck_analogs()
	{
		//read data from nunchuck analog
		float value_y = egm.CurrentWiimote.NUNCHUK_JOY_Y_SPLIT;
		float value_x = egm.CurrentWiimote.NUNCHUK_JOY_X_SPLIT;

//		Debug.Log ("NUNCHUK_JOY_X_SPLIT: "+egm.CurrentWiimote.NUNCHUK_JOY_X_SPLIT);
//		Debug.Log ("NUNCHUK_JOY_Y_SPLIT: "+egm.CurrentWiimote.NUNCHUK_JOY_Y_SPLIT);


		//analog threshold of 0.5 on y-axis
		if (Mathf.Abs(value_y) < 0.5f)
			value_y = 0;
		//analog threshold of 0.5 on x-axis
		if (Mathf.Abs(value_x) < 0.5f)
			value_x = 0;

		//rotate player
		DirXform.Rotate(0, value_x * Time.deltaTime * PlayerRotationSpeed, 0);
//		DirXform.rotation =  Quaternion.Euler(0.0f, value_x * Time.deltaTime * PlayerRotationSpeed, 0.0f);

		//movevement
		DirXform.Translate(0, 0, value_y * Time.deltaTime * PlayerMovementSpeed);
	}

	private void Nunchuck_buttons()
	{
		//present models menu
		if(egm.CurrentWiimote.NUNCHUK_C == 1.0f)
		{
			if(!egm.DisableMenus)
			{
				egm.ActivateSelectionMenu = !egm.ActivateSelectionMenu;
			}
		}
		//select viewed model control
		if(egm.CurrentWiimote.NUNCHUK_Z == 1.0f)
		{
			if(egm.ActivateSelectionMenu)
			{
				//select model
			}
		}
	}

	private void RenderWiimoteKeyStats()
	{
		GUI.BeginGroup(new Rect(10,10, 180,500));
		GUI.Box(new Rect(0,0,130,500), "Wiimote 1:");
		GUI.Label(new Rect(5,20,110,20), "Button A: " + egm.CurrentWiimote.BUTTON_A.ToString() );
		GUI.Label(new Rect(5,35,110,20), "Button B: " + egm.CurrentWiimote.BUTTON_B.ToString());
		GUI.Label(new Rect(5,50,110,20), "Button Left: " + egm.CurrentWiimote.BUTTON_LEFT.ToString());
		GUI.Label(new Rect(5,65,110,20), "Button Right: " + egm.CurrentWiimote.BUTTON_RIGHT.ToString());
		GUI.Label(new Rect(5,80,110,20), "Button Up: " + egm.CurrentWiimote.BUTTON_UP.ToString());
		GUI.Label(new Rect(5,95,110,20), "Button Down: " + egm.CurrentWiimote.BUTTON_DOWN.ToString());
		GUI.Label(new Rect(5,110,110,20), "Button 1: " + egm.CurrentWiimote.BUTTON_ONE.ToString());
		GUI.Label(new Rect(5,125,110,20), "Button 2: " + egm.CurrentWiimote.BUTTON_TWO.ToString());
		GUI.Label(new Rect(5,140,110,20), "Button Plus: " + egm.CurrentWiimote.BUTTON_PLUS.ToString());
		GUI.Label(new Rect(5,155,110,20), "Button Minus: " + egm.CurrentWiimote.BUTTON_MINUS.ToString());
		
		GUI.Label(new Rect(5,170,110,20), "Pitch" + egm.CurrentWiimote.PRY_PITCH.ToString());
		GUI.Label(new Rect(5,185,110,20), "Roll" + egm.CurrentWiimote.PRY_ROLL.ToString());
		GUI.Label(new Rect(5,200,110,20), "Yaw" + egm.CurrentWiimote.PRY_YAW.ToString());
		GUI.Label(new Rect(5,215,110,20), "Accel" + egm.CurrentWiimote.PRY_ACCEL.ToString());
		GUI.Label(new Rect(5,410,110,20), "AX" + egm.CurrentWiimote.ACCX.ToString());
		GUI.Label(new Rect(5,425,110,20), "AY" + egm.CurrentWiimote.ACCY.ToString());
		GUI.Label(new Rect(5,440,110,20), "AZ" + egm.CurrentWiimote.ACCZ.ToString());
		
		GUI.Label(new Rect(5,230,110,20), "Nunchuk:");
		GUI.Label(new Rect(5,245,160,20), "NJX" + egm.CurrentWiimote.NUNCHUK_JOY_X.ToString());
		GUI.Label(new Rect(5,260,160,20), "NJY" + egm.CurrentWiimote.NUNCHUK_JOY_Y.ToString());
		GUI.Label(new Rect(5,275,160,20), "Accx" + egm.CurrentWiimote.NUNCHUK_ACCX.ToString());
		GUI.Label(new Rect(5,290,160,20), "AccY" + egm.CurrentWiimote.NUNCHUK_ACCY.ToString());
		GUI.Label(new Rect(5,305,160,20), "AccZ" + egm.CurrentWiimote.NUNCHUK_ACCZ.ToString());
		GUI.Label(new Rect(5,320,160,20), "P" + egm.CurrentWiimote.NUNCHUK_PITCH.ToString());
		GUI.Label(new Rect(5,335,160,20), "R" + egm.CurrentWiimote.NUNCHUK_ROLL.ToString());
		GUI.Label(new Rect(5,350,160,20), "Y" + egm.CurrentWiimote.NUNCHUK_YAW.ToString());
		GUI.Label(new Rect(5,365,160,20), "A" + egm.CurrentWiimote.NUNCHUK_ACCEL.ToString());
		GUI.Label(new Rect(5,380,160,20), "BUTC" + egm.CurrentWiimote.NUNCHUK_C.ToString());
		GUI.Label(new Rect(5,395,160,20), "BUTZ" + egm.CurrentWiimote.NUNCHUK_Z.ToString());
		GUI.Label(new Rect(5,460,160,20), "IR_X: " + egm.CurrentWiimote.IR_X.ToString());
		GUI.Label(new Rect(5,480,160,20), "IR_Y: " + egm.CurrentWiimote.IR_Y.ToString());
		GUI.EndGroup();
	}

//	private void RenderModelsMenu()
//	{
//		Debug.Log ("RenderModelsMenu activated");
////		GUI.Label(new Rect(Screen.width/2, Screen.height/2, 200, 50), "CENAS E COISAS");
//	}

}
