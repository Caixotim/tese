﻿using UnityEngine;using System.Collections;public class FurniturePropMenu : MonoBehaviour {	private EditorGameManager egm;	private MenuManager mm;	private enum MenuProperty {move, delete, none};	private MenuProperty selectedProperty;	private bool isFirstUpdate;	// Use this for initialization	void Start () {		egm = EditorGameManager.Instance;		mm = MenuManager.Instance;		selectedProperty = MenuProperty.none;		isFirstUpdate = true;	}		// Update is called once per frame	void Update () {		if(mm.ActivatedMenu == MenuManager.Menu.furniture_properties) {			Debug.Log("bp");			if(isFirstUpdate) {				Debug.Log("show first");				SetFurniturePropMenu(true);				isFirstUpdate = false;			}			WiiHandler();		} else {			isFirstUpdate = true;		}	}	private void SetFurniturePropMenu(bool toShow) {		Canvas cv = GameObject.Find("Menu_Properties/Item_block").GetComponent<Canvas>();		cv.enabled = toShow;		if(toShow) {			mm.ActivatedMenu = MenuManager.Menu.furniture_properties;		} else {			selectedProperty = MenuProperty.none;			mm.ActivatedMenu = MenuManager.Menu.none;		}	}	private void WiiHandler () {		NunchuckMenuHandle();		WiimoteMenuHandle();	}	private void WiimoteMenuHandle() {		if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.BUTTON_A) && mm.ActivatedMenu == MenuManager.Menu.furniture_properties) {			switch(selectedProperty) {				case MenuProperty.move:					//move					Debug.Log("move");					SetFurniturePropMenu(false);					break;				case MenuProperty.delete:					//delete furniture					Debug.Log("delete");					egm.GrabbedObject.GetComponent<FurnitureHandler>().DeleteFurniture();					SetFurniturePropMenu(false);					break;			}		}		if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_C) && mm.ActivatedMenu == MenuManager.Menu.furniture_properties) {			egm.GrabbedObject.GetComponent<FurnitureHandler>().DropFurniture();			SetFurniturePropMenu(false);		}	}	private void NunchuckMenuHandle() {		if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_LEFT)) {			//highlight property			selectedProperty = MenuProperty.delete;		}		else if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_RIGHT)) {			//highlight property			selectedProperty = MenuProperty.move;		} /*else {			//remove highlight on all			selectedProperty = MenuProperty.none;		}*/		// Debug.Log(selectedProperty);	}}