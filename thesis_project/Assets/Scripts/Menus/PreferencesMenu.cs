using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreferencesMenu : MonoBehaviour {

	private EditorGameManager egm;
	private SimulationManager sm;
	private MenuManager mm;
	private Transform player;
	private int userHeight;
	private int height_delta = 10;

	void Start () {
		egm = EditorGameManager.Instance;
		sm = SimulationManager.Instance;
		mm = MenuManager.Instance;
		player = GameObject.Find("Player").transform;
		userHeight = sm.UserHeight;
	}
	
	void Update () {
		WiiHandler();
	}

	private void WiiHandler () {
		NunchuckMenuHandle();
		WiimoteMenuHandle();
	}

	private void NunchuckMenuHandle () {
		if (egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_Z) && egm.GrabbedObject == null && (mm.ActivatedMenu == MenuManager.Menu.user_preferences || mm.ActivatedMenu == MenuManager.Menu.none)) {
			ToggleMenu();
		}

		if(mm.ActivatedMenu == MenuManager.Menu.user_preferences) {
			if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_UP)) {
				IncreaseHeight();
			}
			if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_DOWN)) {
				DecreaseHeight();
			}
		}
	}

	private void WiimoteMenuHandle() {
		if(mm.ActivatedMenu == MenuManager.Menu.user_preferences && egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.BUTTON_A)) {
			ApplyChanges();
		}
	}

	private void ApplyChanges() {
		//user height
		Debug.Log("New user height: " + userHeight/100.0f);
		sm.UserHeight = userHeight;
		player.transform.position = new Vector3(player.transform.position.x, sm.UserHeight/100.0f, player.transform.position.z);
	}

	private void IncreaseHeight() {
		userHeight += height_delta;
		UpdateMenu();
	}

	private void DecreaseHeight() {
		userHeight -= height_delta;
		UpdateMenu();
	}

	private void UpdateMenu() {
		Text height_txt = GameObject.Find("Menu_Preferences/Item_block/Body/Panel/Value").GetComponent<Text>();
		height_txt.text = ""+userHeight;
	}

	private void ToggleMenu() {
		Canvas cv = GameObject.Find("Menu_Preferences/Item_block").GetComponent<Canvas>();
		cv.enabled = !cv.enabled;
		if(cv.enabled) {
			UpdateMenu();
			mm.ActivatedMenu = MenuManager.Menu.user_preferences;
		} else {
			mm.ActivatedMenu = MenuManager.Menu.none;
		}
	}
}