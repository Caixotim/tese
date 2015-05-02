using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour {

	private MenuManager mm;
	public GameObject cam;
	private float object_distance;
	private EditorGameManager egm;
	private SimulationManager sm;
	private int currentFurnitureIndex;
	private float menu_items_create_distance = 6.0f;
	
	void Start () {
		egm = EditorGameManager.Instance;
		sm = SimulationManager.Instance;
		mm = MenuManager.Instance;
		object_distance = 10.0f;
		currentFurnitureIndex = 0;
		SetMenuFurnitureInfo();
	}

	void Update () {
		if(sm.SelectedController == SimulationManager.Controller.wiiremote) {
			WiiHandler();
		}
	}

	private void WiiHandler () {
		NunchuckMenuHandle();
		WiimoteMenuHandle();
	}

	private void WiimoteMenuHandle() {
		if(mm.ActivatedMenu == MenuManager.Menu.furniture_select) {
			if(egm.CurrentWiimote.BUTTON_A == 1.0f) {
				//create furniture in front of user
				Furniture selectedFurniture = egm.Furnitures[currentFurnitureIndex];

				GameObject newGameObject = (GameObject)Resources.LoadAssetAtPath("Assets/Resources/Furniture/Models/"+selectedFurniture.Id+"/"+selectedFurniture.Id+".prefab", typeof(GameObject));
				

				//set object position to be in front of the player
				Vector3 normalized_obj_vector = Vector3.Normalize (cam.transform.forward);
		
				float Nx = cam.transform.localPosition.x + (normalized_obj_vector.x * menu_items_create_distance);
				float Ny = cam.transform.localPosition.y + (normalized_obj_vector.y * menu_items_create_distance);
				float Nz = cam.transform.localPosition.z + (normalized_obj_vector.z * menu_items_create_distance);

				newGameObject.transform.position = new Vector3(Nx,Ny,Nz);

				Instantiate(newGameObject);
				egm.GrabbedObject = newGameObject;

				ToggleMenu();
			}
		}
	}

	private void NunchuckMenuHandle() {
		//read data from nunchuck analog
		float nunchuck_analog_x = egm.CurrentWiimote.NUNCHUK_JOY_X_SPLIT;
		//normalize values
		if(Mathf.Abs(nunchuck_analog_x) < 0.8f) {
			nunchuck_analog_x = 0;
		}


		
	
		if (egm.CurrentWiimote.NUNCHUK_C == 1.0f && egm.GrabbedObject == null) //Activate/Deactivate selection menu
		{
			ToggleMenu();
		}

		if(mm.ActivatedMenu == MenuManager.Menu.furniture_select) {
			if(nunchuck_analog_x > 0) { //next
				NextFurniture();
			}
			else if(nunchuck_analog_x < 0) { //previous
				PreviousFurniture();
			}
		}
	}

	private void SetMenuFurnitureInfo()
	{
		Furniture furnitureData = egm.Furnitures [currentFurnitureIndex];

			Text title_txt = GameObject.Find("Menu_Selection/Item_block/Title/Text").GetComponent<Text> ();;
			title_txt.text = furnitureData.Name;


			Text price_txt = GameObject.Find ("Menu_Selection/Item_block/Body/Price").GetComponent<Text> ();;
			price_txt.text = "PREÇO" + ": " + furnitureData.Price + furnitureData.Currency;

			Image image_img = GameObject.Find ("Menu_Selection/Item_block/Body/Image").GetComponent<Image> ();;
			image_img.overrideSprite = (Sprite)Resources.LoadAssetAtPath("Assets/Resources/Furniture/Images/"+furnitureData.Id+".jpeg", typeof(Sprite));
			// if(image_img.overrideSprite != null)
			// 	Debug.Log(image_img.overrideSprite.name);
			// else
			// 	Debug.Log("Sprite not defined!!! "+furnitureData.Id+".jpeg");
	}

	private void NextFurniture (){
		if(currentFurnitureIndex < egm.Furnitures.Length-1) {
			currentFurnitureIndex++;
		} else {
			currentFurnitureIndex = 0;
		}
		SetMenuFurnitureInfo();
	}

	private void PreviousFurniture (){
		if(currentFurnitureIndex > 0) {
			currentFurnitureIndex--;
		} else {
			currentFurnitureIndex = egm.Furnitures.Length-1;
		}
		SetMenuFurnitureInfo();
	}

	private void ToggleMenu () {
		Canvas cv = GameObject.Find("Menu_Selection/Item_block").GetComponent<Canvas>();
		cv.enabled = !cv.enabled;
		if(cv.enabled) {
			mm.ActivatedMenu = MenuManager.Menu.furniture_select;
		}
		else {
			mm.ActivatedMenu = MenuManager.Menu.none;
		}
	}

}
