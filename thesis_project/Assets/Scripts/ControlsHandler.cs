	using UnityEngine;
	using System.Collections;

	public class ControlsHandler : MonoBehaviour {

		private WiimoteReceiver receiver;
		//Player settings
		private float PlayerMovementSpeed = 2;
		private float PlayerRotationSpeed = 100;

		public Texture cursorOne;
		public Transform portalGun;
		private Transform grabbedObj;
		private string objectFrontName;
		private Transform DirXform;
		private EditorGameManager egm;
		private SimulationManager sm;

		private float grabbedObj_size;

		void Start () {
			egm = EditorGameManager.Instance;
			sm = SimulationManager.Instance;

			receiver = new WiimoteReceiver();
			receiver.connect();
			portalGun.forward = Vector3.Normalize(Vector3.forward);

			DirXform = GameObject.Find ("OVRPlayerController").transform;
			
			Debug.Log(sm.SelectedController);

			if(sm.SelectedController == SimulationManager.Controller.wiiremote) {
				//to ensure that wiiremote is instanciated properly
				while(true)
				{
					if(SetWiiMote ()) {
						return;
					}
				}
			}
		}
		
		// Update is called once per frame
		void Update () {
			if (sm.SelectedController == SimulationManager.Controller.wiiremote) {
				if(egm.CurrentWiimote != null)
				{
					WiiControls();
					NunchuckControls();
				}
			} else if (sm.SelectedController == SimulationManager.Controller.keyboardMouse) {
				MouseControls();
				KeyboardControls();
			}
		}

		private void WiiControls(){
			float delta_x = egm.CurrentWiimote.PRY_YAW;
			float delta_y = egm.CurrentWiimote.PRY_PITCH;
			float delta_z = 0;
			float? delta_x_prev = null;
			float? delta_y_prev = null;
	
			if (Mathf.Abs (delta_x - 0.5f) < 0.3f || (delta_x_prev != null && delta_x_prev == delta_x))
				delta_x = 0;
			else
				delta_x -= 0.5f;
			if (Mathf.Abs (delta_y - 0.515f) < 0.3f || (delta_y_prev != null && delta_y_prev == delta_y))
				delta_y = 0;
			else
				delta_y -= 0.5f;

			//Controll gun orientation by IR sensor
		float wiimote_zPos = CalculateZPos ();
		Vector3 crosshairPos = Camera.allCameras[0].ScreenToWorldPoint(new Vector3(egm.CurrentWiimote.IR_X * Screen.width, egm.CurrentWiimote.IR_Y * Screen.height, wiimote_zPos));

			portalGun.LookAt(crosshairPos);

			//apply raycast
			RaycastHit hit;
			Ray ray = new Ray ();
			ray.origin = portalGun.position;
			ray.direction = portalGun.forward;
			if(Physics.Raycast(ray, out hit, 50.0f))
			{
				if(hit.transform.tag.Equals("furniture") && egm.CurrentWiimote.BUTTON_A == 1.0f) {
					FurnitureHandler grabDropObject = hit.transform.GetComponent<FurnitureHandler>();

					if(grabDropObject != null) {
						if(grabbedObj == null)
						{
							grabbedObj = grabDropObject.GrabFurniture();
							grabbedObj.parent = portalGun.transform;
						}
						else if(grabbedObj != null)
						{
							if(grabDropObject.DropFurniture()) {
								grabbedObj.parent = null;
								grabbedObj = null;
							}
						}
					}
				}
			}
		}

		private void NunchuckControls()
		{
			Nunchuck_analogs ();
			Nunchuck_buttons ();
		}

		private void MouseControls()
		{
			RaycastHit hit;
			Vector3 mousePos = Input.mousePosition; 
			mousePos.z = 0.5f;
			Vector3 worldPos = Camera.allCameras[0].ScreenToWorldPoint(mousePos);

			portalGun.LookAt(worldPos);

			Ray ray = new Ray ();
			ray.origin = portalGun.position;
			ray.direction = portalGun.forward;
			if(Physics.Raycast(ray, out hit, 50.0f))
			{
				if(hit.transform.tag.Equals("furniture") && Input.GetMouseButtonDown(0)) {
					FurnitureHandler grabDropObject = hit.transform.GetComponent<FurnitureHandler>();

					if(grabDropObject != null) {
						if(grabbedObj == null)
						{
							grabbedObj = grabDropObject.GrabFurniture();
							grabbedObj.parent = portalGun.transform;
						}
						else if(grabbedObj != null)
						{
							if(grabDropObject.DropFurniture()) {
								grabbedObj.parent = null;
								grabbedObj = null;
							}
						}
					}
				}
			}
		}

		private void KeyboardControls () {
			float value_x = 0;
			float value_y = 0;

			//Y-axys movement
			if(Input.GetKey(KeyCode.W)) {
				value_y = 0.5f;
			} else if(Input.GetKey(KeyCode.S)) {
				value_y = -0.5f;
			}
			//Rotation
			if(Input.GetKey(KeyCode.A)) {
				value_x = -0.5f;
			} else if(Input.GetKey(KeyCode.D)) {
				value_x = 0.5f;
			}

			//rotate player
			DirXform.Rotate(0, value_x * Time.deltaTime * PlayerRotationSpeed, 0);

			//movevement
			DirXform.Translate(0, 0, value_y * Time.deltaTime * PlayerMovementSpeed);
		}

		//Process nunchuck analog controls
		private void Nunchuck_analogs()
		{
			//read data from nunchuck analog
			float value_y = egm.CurrentWiimote.NUNCHUK_JOY_Y_SPLIT;
			float value_x = egm.CurrentWiimote.NUNCHUK_JOY_X_SPLIT;

			//analog threshold of 0.5 on y-axis
			if (Mathf.Abs(value_y) < 0.5f)
				value_y = 0;
			//analog threshold of 0.5 on x-axis
			if (Mathf.Abs(value_x) < 0.5f)
				value_x = 0;

			//rotate player
			DirXform.Rotate(0, value_x * Time.deltaTime * PlayerRotationSpeed, 0);

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

	private float CalculateZPos() {
		float radiansPerPixel = (float)(Mathf.PI / 4) / 1024.0f;
		float dotDistanceInMM = 234.0f;//distância entre o centro dos leds dos óculos = 234.0f, wiiSensorBar = 215.9f
		float movementScaling = 1.0f;
		float screenHeightInMm = 20.0f;

		float zPos = 0.5f;

		Vector2 firstPoint = new Vector2();
		Vector2 secondPoint = new Vector2();
		int numvisible = 0;
		
		if(egm.CurrentWiimote.IR_1_SIZE == 1.0f) {
			
			firstPoint.x = egm.CurrentWiimote.IR_1_X;
			firstPoint.y = egm.CurrentWiimote.IR_1_Y;
			numvisible = 1;
		}
		if(egm.CurrentWiimote.IR_2_SIZE == 1.0f) {
			if(numvisible == 0) {
				firstPoint.x = egm.CurrentWiimote.IR_2_X;
				firstPoint.y = egm.CurrentWiimote.IR_2_Y;
				numvisible = 1;
			}
			else {
				secondPoint.x = egm.CurrentWiimote.IR_2_X;
				secondPoint.y = egm.CurrentWiimote.IR_2_Y;
				numvisible = 2;
			}
		}
		if(egm.CurrentWiimote.IR_3_SIZE == 1.0f) {
			if(numvisible == 0) {
				firstPoint.x = egm.CurrentWiimote.IR_3_X;
				firstPoint.y = egm.CurrentWiimote.IR_3_Y;
				numvisible = 1;
			}
			else if(numvisible == 1) {
				secondPoint.x = egm.CurrentWiimote.IR_3_X;
				secondPoint.y = egm.CurrentWiimote.IR_3_Y;
				numvisible = 2;
			}
		}
		if(egm.CurrentWiimote.IR_4_SIZE == 1.0f) {
			if(numvisible == 1) {
				secondPoint.x = egm.CurrentWiimote.IR_4_X;
				secondPoint.y = egm.CurrentWiimote.IR_3_Y;
				numvisible = 2;
			}
		}

		if(numvisible == 2) {
			float dx = firstPoint.x - secondPoint.x;
			float dy = firstPoint.y - secondPoint.y;
			float pointDist = (float)Mathf.Sqrt(dx * dx + dy * dy);

			float angle = radiansPerPixel * pointDist / 2.0f;
			float distLedsAoComandoEmMm = (float)((dotDistanceInMM / 2.0f) / Mathf.Tan(angle));

			zPos = movementScaling * (distLedsAoComandoEmMm / screenHeightInMm); //define a posição Z do utilizador
		}

		return zPos;
	}
