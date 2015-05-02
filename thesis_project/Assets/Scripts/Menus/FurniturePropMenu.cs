using UnityEngine;
using System.Collections;

public class FurniturePropMenu : MonoBehaviour {

	private EditorGameManager egm;
	private SimulationManager sm;
	private MenuManager mm;
	private enum MenuProperty {move, delete, none};
	private MenuProperty selectedProperty;


	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		sm = SimulationManager.Instance;
		mm = MenuManager.Instance;
		selectedProperty = MenuProperty.none;
	}
	
	// Update is called once per frame
	void Update () {
		if(mm.ActivatedMenu == MenuManager.Menu.furniture_properties) {
			WiiHandler();
			SetFurniturePropMenu(true);
		}
	}

	private void SetFurniturePropMenu(bool toShow) {
		Canvas cv = GameObject.Find("Menu_Properties/Item_block").GetComponent<Canvas>();
		if(toShow) {
			mm.ActivatedMenu = MenuManager.Menu.furniture_properties;
			if(!cv.enabled) {
				cv.enabled = toShow;
				mm.ActivatedMenu = MenuManager.Menu.furniture_properties;
			}
		} else {
			if(!cv.enabled) {
				mm.ActivatedMenu = MenuManager.Menu.none;
				cv.enabled = toShow;
			}
		}
	}

	private void WiiHandler () {
		NunchuckMenuHandle();
		WiimoteMenuHandle();
	}

	private void WiimoteMenuHandle() {
		if(egm.CurrentWiimote.BUTTON_A == 1.0f) {
			Debug.Log(selectedProperty);
			switch(selectedProperty) {
				case MenuProperty.move:
					//move
					SetFurniturePropMenu(false);
					break;
				case MenuProperty.delete:
					//delete furniture
					egm.GrabbedObject.GetComponent<FurnitureHandler>().DeleteFurniture();
					SetFurniturePropMenu(false);
					break;
			}
		}
		else if(egm.CurrentWiimote.NUNCHUK_C == 1.0f) {
			egm.GrabbedObject.GetComponent<FurnitureHandler>().DropFurniture();
			SetFurniturePropMenu(false);
		}
	}

	private void NunchuckMenuHandle() {
		//read data from nunchuck analog
		float nunchuck_analog_x = egm.CurrentWiimote.NUNCHUK_JOY_X_SPLIT;
		//normalize values
		if(Mathf.Abs(nunchuck_analog_x) < 0.5f) {
			nunchuck_analog_x = 0;
		}

		if(nunchuck_analog_x > 0) {
			selectedProperty = MenuProperty.move;
		}
		else if(nunchuck_analog_x < 0) {
			selectedProperty = MenuProperty.delete;
		}
	}
}
