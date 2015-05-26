﻿using UnityEngine;using System.Collections;using UnityEngine.UI;public class FurniturePropMenu : MonoBehaviour {	private EditorGameManager egm;	private MenuManager mm;	private enum MenuProperty {move, delete, cancel};	private MenuProperty[] properties = {MenuProperty.move, MenuProperty.delete, MenuProperty.cancel};	private int selectedPropertyIndex;	private bool isFirstUpdate;	public Shader outlineShader;	private string baseMenuItemPath = "Player/OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Menu_Properties/Item_block/";	private Color highLightColor = new Color(0.1f, 0.7f, 0.4f);	private Color baseColor = Color.yellow; //just to check if already retrieved	// Use this for initialization	void Start () {		egm = EditorGameManager.Instance;		mm = MenuManager.Instance;		isFirstUpdate = true;		selectedPropertyIndex = 0;	}		// Update is called once per frame	void Update () {		if(mm.ActivatedMenu == MenuManager.Menu.furniture_properties) {			if(isFirstUpdate) {				SetFurniturePropMenu(true);				HighlightMenuItem();				isFirstUpdate = false;			}			WiiHandler();		} else {			isFirstUpdate = true;		}	}	private void SetFurniturePropMenu(bool toShow) {		Canvas cv = GameObject.Find("Menu_Properties/Item_block").GetComponent<Canvas>();		cv.enabled = toShow;		if(toShow) {			mm.ActivatedMenu = MenuManager.Menu.furniture_properties;		} else {			mm.ActivatedMenu = MenuManager.Menu.none;		}	}	private void WiiHandler () {		NunchuckMenuHandle();		WiimoteMenuHandle();		}	private void WiimoteMenuHandle() {		// Debug.Log(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.BUTTON_A));		if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.BUTTON_A)) {			Debug.Log("Button A pressed");			switch(properties[selectedPropertyIndex]) {				case MenuProperty.move:					break;				case MenuProperty.delete:					egm.GrabbedObject.GetComponent<FurnitureHandler>().DeleteFurniture();					break;				case MenuProperty.cancel:					egm.GrabbedObject.GetComponent<FurnitureHandler>().DropFurniture();					break;			}			SetFurniturePropMenu(false);		}	}	private void NunchuckMenuHandle() {		if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_DOWN)) {			if(selectedPropertyIndex < properties.Length - 1) {				selectedPropertyIndex ++;				HighlightMenuItem();			}		}		else if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_UP)) {			if(selectedPropertyIndex > 0) {				selectedPropertyIndex --;				HighlightMenuItem();			}		}	}	private void HighlightMenuItem () {		Image move_img;		Image delete_img;		Image cancel_img;		switch(properties[selectedPropertyIndex]) {			case MenuProperty.move:				//highlight move item				Debug.Log("MOVE");				move_img = GameObject.Find(baseMenuItemPath + "Move").GetComponent<Image>();				SaveImageBaseColor(move_img.color);				move_img.color = highLightColor;				delete_img = GameObject.Find(baseMenuItemPath + "Delete").GetComponent<Image>();				delete_img.color = baseColor;				cancel_img = GameObject.Find(baseMenuItemPath + "Cancel").GetComponent<Image>();				cancel_img.color = baseColor;				break;			case MenuProperty.delete:				//highlight delete item				Debug.Log("DELETE");				delete_img = GameObject.Find(baseMenuItemPath + "Delete").GetComponent<Image>();				SaveImageBaseColor(delete_img.color);				delete_img.color = highLightColor;				move_img = GameObject.Find(baseMenuItemPath + "Move").GetComponent<Image>();				move_img.color = baseColor;				cancel_img = GameObject.Find(baseMenuItemPath + "Cancel").GetComponent<Image>();				cancel_img.color = baseColor;				break;			case MenuProperty.cancel:				//highlight cancel item				Debug.Log("CANCEL");				cancel_img = GameObject.Find(baseMenuItemPath + "Cancel").GetComponent<Image>();				SaveImageBaseColor(cancel_img.color);				cancel_img.color = highLightColor;				move_img = GameObject.Find(baseMenuItemPath + "Move").GetComponent<Image>();				move_img.color = baseColor;				delete_img = GameObject.Find(baseMenuItemPath + "Delete").GetComponent<Image>();				delete_img.color = baseColor;				break;		}	}	private void SaveImageBaseColor (Color _baseColor_) {		if (baseColor == Color.yellow) {			baseColor = _baseColor_;		}	}}