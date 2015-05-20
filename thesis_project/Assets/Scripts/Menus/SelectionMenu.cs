﻿using UnityEngine;using System.Collections;using UnityEngine.UI;public class SelectionMenu : MonoBehaviour {	private MenuManager mm;	public GameObject cam;	private EditorGameManager egm;	private int currentFurnitureIndex;	private float object_distance = 1.5f;	private Transform portalGun;		void Start () {		egm = EditorGameManager.Instance;		mm = MenuManager.Instance;		currentFurnitureIndex = 0;		SetMenuFurnitureInfo();		portalGun = GameObject.Find("PortalGunPivot").transform;	}	void Update () {		WiiHandler();	}	private void WiiHandler () {		NunchuckMenuHandle();		WiimoteMenuHandle();	}	private void WiimoteMenuHandle() {		if(mm.ActivatedMenu == MenuManager.Menu.furniture_select) {			if(egm.CurrentWiimote.BUTTON_A == 1.0f) {				//create furniture in front of user				Furniture selectedFurniture = egm.Furnitures[currentFurnitureIndex];				GameObject newGameObject = (GameObject)Resources.LoadAssetAtPath("Assets/Resources/Furniture/Models/"+selectedFurniture.Id+"/"+selectedFurniture.Id+".prefab", typeof(GameObject));				var object_position = cam.transform.position+(cam.transform.forward*2)*object_distance;				object_position = new Vector3(object_position.x, 0.5f, object_position.z);				GameObject furniture = (GameObject) Instantiate(newGameObject, object_position, cam.transform.rotation);				FurnitureHandler fh = furniture.GetComponent<FurnitureHandler>();				//set furniture info				fh.FurnitureInfo = selectedFurniture;				//increase budget				egm.Budget = egm.Budget + fh.FurnitureInfo.Price;				fh.GrabFurniture(portalGun);				ToggleMenu();			}		}	}	private void NunchuckMenuHandle() {		//Activate/Deactivate selection menu (only if no object is being grabbed)		if (egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_C) && egm.GrabbedObject == null && (mm.ActivatedMenu == MenuManager.Menu.furniture_select || mm.ActivatedMenu == MenuManager.Menu.none)) {			ToggleMenu();		}		//If activated handle right/left logic		if(mm.ActivatedMenu == MenuManager.Menu.furniture_select) {			if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_RIGHT)) { //next furniture				NextFurniture();			}			else if(egm.CurrentWiimote.GetKeyPress((int)Wiimote.KeyCode.NUNCHUCK_ANALOG_LEFT)) { //previous furniture				PreviousFurniture();			}		}	}	private void SetMenuFurnitureInfo()	{		Furniture furnitureData = egm.Furnitures [currentFurnitureIndex];		Text title_txt = GameObject.Find("Menu_Selection/Item_block/Title/Text").GetComponent<Text> ();		title_txt.text = furnitureData.Name;		Text price_txt = GameObject.Find ("Menu_Selection/Item_block/Body/Price").GetComponent<Text> ();		price_txt.text = "PREÇO" + ": " + furnitureData.Price + furnitureData.Currency;		Image image_img = GameObject.Find ("Menu_Selection/Item_block/Body/Image").GetComponent<Image> ();		image_img.overrideSprite = (Sprite)Resources.LoadAssetAtPath("Assets/Resources/Furniture/Images/"+furnitureData.Id+".jpeg", typeof(Sprite));	}	private void NextFurniture (){		if(currentFurnitureIndex < egm.Furnitures.Length-1) {			currentFurnitureIndex++;		} else {			currentFurnitureIndex = 0;		}		SetMenuFurnitureInfo();	}	private void PreviousFurniture (){		if(currentFurnitureIndex > 0) {			currentFurnitureIndex--;		} else {			currentFurnitureIndex = egm.Furnitures.Length-1;		}		SetMenuFurnitureInfo();	}	private void ToggleMenu () {		Canvas cv = GameObject.Find("Menu_Selection/Item_block").GetComponent<Canvas>();		cv.enabled = !cv.enabled;		if(cv.enabled) {			mm.ActivatedMenu = MenuManager.Menu.furniture_select;		}		else {			mm.ActivatedMenu = MenuManager.Menu.none;		}	}}