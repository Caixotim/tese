using UnityEngine;using System.Collections;public class ControlsHandler : MonoBehaviour {		private WiimoteReceiver receiver;	//Player settings	private float PlayerMovementSpeed = 2;	private float PlayerRotationSpeed = 100;	public Transform portalGun;	private string objectFrontName;	private Transform DirXform;	private EditorGameManager egm;	private SimulationManager sm;	private MenuManager mm;	private Animator playerAnim;		void Start () {		egm = EditorGameManager.Instance;		sm = SimulationManager.Instance;		mm = MenuManager.Instance;				receiver = new WiimoteReceiver ();		receiver.connect ();		portalGun.forward = Vector3.Normalize (Vector3.forward);				DirXform = GameObject.Find ("Player").transform;				// Debug.Log (sm.SelectedController);				if (sm.SelectedController == SimulationManager.Controller.wiiremote) {			//to ensure that wiiremote is instanciated properly			while (true) {				if (SetWiiMote ()) {					return;				}			}		}	}		void Update () {		if (sm.SelectedController == SimulationManager.Controller.wiiremote) {			// if (egm.CurrentWiimote != null && mm.ActivatedMenu == MenuManager.Menu.none) {				WiiControls ();				NunchuckControls ();			// }		} else if (sm.SelectedController == SimulationManager.Controller.keyboardMouse) {			MouseControls ();			KeyboardControls ();		}	}		private void WiiControls () {		float delta_x = egm.CurrentWiimote.PRY_YAW;		float delta_y = egm.CurrentWiimote.PRY_PITCH;		float delta_z = 0;		float? delta_x_prev = null;		float? delta_y_prev = null;		bool isGrabbingObject = false;				if (Mathf.Abs (delta_x - 0.5f) < 0.3f || (delta_x_prev != null && delta_x_prev == delta_x))			delta_x = 0;		else			delta_x -= 0.5f;		if (Mathf.Abs (delta_y - 0.515f) < 0.3f || (delta_y_prev != null && delta_y_prev == delta_y))			delta_y = 0;		else			delta_y -= 0.5f;				//Controll gun orientation by IR sensor		Vector3 crosshairPos = Camera.allCameras [0].ScreenToWorldPoint (new Vector3 (egm.CurrentWiimote.IR_X * Screen.width, egm.CurrentWiimote.IR_Y * Screen.height, 5.0f));				portalGun.LookAt (crosshairPos);						if (egm.CurrentWiimote.GetKeyPress((int) Wiimote.KeyCode.BUTTON_A)) {			bool isActionFinished = false;			//apply raycast			RaycastHit hit;			Ray ray = new Ray ();			ray.origin = portalGun.position;			ray.direction = portalGun.forward;			if (Physics.Raycast (ray, out hit, 50.0f)) {				if (egm.GrabbedObject == null && (hit.transform.tag.Equals ("furniture") || hit.transform.tag.Equals("select"))) {					FurnitureHandler grabDropObject = hit.transform.GetComponent<FurnitureHandler> ();					grabDropObject.GrabFurniture (portalGun.transform);					isActionFinished = true;				} else if (hit.transform.tag.Equals("interactable")) {					if (mm.ActivatedMenu == MenuManager.Menu.furniture_select) {						//arrows						SelectionMenu selectMenu = GameObject.Find("Menu_Interaction").GetComponent<SelectionMenu>();						selectMenu.DoAction(hit.transform.name);						isActionFinished = true;					} else if (mm.ActivatedMenu == MenuManager.Menu.user_preferences) {						//arrows						HeightMenu heightMenu = GameObject.Find("Menu_Interaction").GetComponent<HeightMenu>();						heightMenu.DoAction(hit.transform.name);						isActionFinished = true;					}				}			}			if (egm.GrabbedObject != null && !isActionFinished) {				FurnitureHandler grabDropObject = egm.GrabbedObject.GetComponent<FurnitureHandler> ();				grabDropObject.DropFurniture ();			}		}	}		private void NunchuckControls () {		Nunchuck_analogs ();		Nunchuck_buttons ();	}		private void MouseControls () {		RaycastHit hit;		Vector3 mousePos = Input.mousePosition; 		mousePos.z = 0.5f;		Vector3 worldPos = Camera.allCameras [0].ScreenToWorldPoint (mousePos);		bool isGrabbingObject = false;				portalGun.LookAt (worldPos);				Ray ray = new Ray ();		ray.origin = portalGun.position;		ray.direction = portalGun.forward;		if (Physics.Raycast (ray, out hit, 50.0f)) {			if (hit.transform != null && hit.transform.tag.Equals ("furniture") && Input.GetMouseButtonDown (0)) {				if (egm.GrabbedObject == null) { //if no object is grabbed					FurnitureHandler objectHandler = hit.transform.GetComponent<FurnitureHandler> ();					objectHandler.GrabFurniture (portalGun.transform);					if(egm.GrabbedObject != null) {						isGrabbingObject = true;					}				}			}		}		if (Input.GetMouseButtonDown (0) && egm.GrabbedObject != null && !isGrabbingObject) { //if an object is grabbed			FurnitureHandler grabDropObject = egm.GrabbedObject.GetComponent<FurnitureHandler> ();			grabDropObject.DropFurniture ();			if(egm.GrabbedObject == null) {				isGrabbingObject = false;			}		}	}		private void KeyboardControls () {		float value_x = 0;		float value_y = 0;				//Y-axys movement		if (Input.GetKey (KeyCode.W)) {			value_y = 0.5f;		} else if (Input.GetKey (KeyCode.S)) {			value_y = -0.5f;		}		//Rotation		if (Input.GetKey (KeyCode.A)) {			value_x = -0.5f;		} else if (Input.GetKey (KeyCode.D)) {			value_x = 0.5f;		}				//rotate player		DirXform.Rotate (0, value_x * Time.deltaTime * PlayerRotationSpeed, 0);				//movevement		DirXform.Translate (0, 0, value_y * Time.deltaTime * PlayerMovementSpeed);		// playerAnim.SetFloat("Speed", value_y);	}		//Process nunchuck analog controls	private void Nunchuck_analogs () {		//read data from nunchuck analog		float value_y = egm.CurrentWiimote.NUNCHUK_JOY_Y_SPLIT;		float value_x = egm.CurrentWiimote.NUNCHUK_JOY_X_SPLIT;				//analog threshold of 0.5 on y-axis		if (Mathf.Abs (value_y) < 0.5f)			value_y = 0;		//analog threshold of 0.5 on x-axis		if (Mathf.Abs (value_x) < 0.5f)			value_x = 0;				//rotate player		DirXform.Rotate (0, value_x * Time.deltaTime * PlayerRotationSpeed, 0);				//movevement		DirXform.Translate (0, 0, value_y * Time.deltaTime * PlayerMovementSpeed);	}		private void Nunchuck_buttons () {		//Selection Menu		if (egm.CurrentWiimote.NUNCHUK_C == 1.0f) {					}		//Settings Menu		if (egm.CurrentWiimote.NUNCHUK_Z == 1.0f) {					}	}		//Set Wiimote on EditorGameManager	public bool SetWiiMote () {		if (receiver.wiimotes.ContainsKey (1)) {			egm.CurrentWiimote = receiver.wiimotes [1];			return true;		}		return false;	}}