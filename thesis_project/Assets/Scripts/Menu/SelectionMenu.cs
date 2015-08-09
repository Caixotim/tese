using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {

	private EditorGameManager egm;
	private MenuManager mm;
	private int selectedFurnitureIndex;
	private float distanceToFurnitureSelect = 1.5f;
	private bool drawnMenu = false;
	private Vector3 furnitureSelectPos = Vector3.zero;
	private Transform player;

	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		selectedFurnitureIndex = 0;
		player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (egm.CurrentWiimote.GetKeyPress((int) Wiimote.KeyCode.NUNCHUCK_C)) {
			ShowSelectionMenu(mm.ActivatedMenu == MenuManager.Menu.none);
		}
	}

	public void DoAction (string direction) {
		switch(direction) {
			case "RightArrow":
				SelectRight();
				break;
			case "LeftArrow":
				SelectLeft();
				break;
		}
	}

	private void SelectRight() {
		if (selectedFurnitureIndex < egm.Furnitures.Length - 1) {
			selectedFurnitureIndex++;
		} else {
			selectedFurnitureIndex = 0;
		}
		DrawSelectionMenu(true);
	}

	private void SelectLeft() {
		if (selectedFurnitureIndex > 0) {
			selectedFurnitureIndex--;
		} else {
			selectedFurnitureIndex = egm.Furnitures.Length - 1;
		}
		DrawSelectionMenu(true);
	}

	private void ShowSelectionMenu (bool isToShow) {
		if (isToShow && !drawnMenu) {
			if (mm.CanDrawMenu) {
				//Show selection menu
				DrawSelectionMenu(true);
			} else {
				// WarningMessage();
			}
		} else if (mm.ActivatedMenu == MenuManager.Menu.furniture_select) {
			//Hide selection menu
			DrawSelectionMenu(false);
		}
	}

	private void DrawSelectionMenu(bool toDraw) {
		GameObject trigger = GameObject.Find("Menu_Trigger");

		if (toDraw) {
			Vector3 furniturePos;

			furniturePos = drawnMenu ? furnitureSelectPos : trigger.transform.position;

			//clean previous object
			CleanPreviousFurniture();

			//draw selection menu
			GameObject furniture = (GameObject) Resources.Load("Furniture/" + (selectedFurnitureIndex + 1) + "/" + 	(selectedFurnitureIndex + 1), typeof(GameObject));
			furniture.transform.tag = "select";

			GameObject instantiatedFurniture = (GameObject) Instantiate(furniture, furniturePos, Quaternion.identity);
			furnitureSelectPos = instantiatedFurniture.transform.position;
			instantiatedFurniture.transform.LookAt(player);
			DrawArrows(instantiatedFurniture.transform.position);
			
			mm.ActivatedMenu = MenuManager.Menu.furniture_select;
			drawnMenu = true;

			mm.CanDrawMenu = false;
		} else {
			//hide selection menu
			HideMenu();

			mm.ActivatedMenu = MenuManager.Menu.none;
			drawnMenu = false;
			mm.CanDrawMenu = true;
		}
	}

	private void DrawArrows (Vector3 centerPosition) {
		GameObject rightArrow = GameObject.Find("RightArrow");
		GameObject leftArrow = GameObject.Find("LeftArrow");

		if (rightArrow == null && leftArrow == null) {
			GameObject arrows = (GameObject) Resources.Load("Arrows/Arrows", typeof(GameObject));
			GameObject instantiatedArrows = (GameObject) Instantiate(arrows, centerPosition, Quaternion.identity);
			instantiatedArrows.transform.name = "Arrows";
			instantiatedArrows.transform.LookAt(player);
			instantiatedArrows.transform.forward = -instantiatedArrows.transform.forward;
		}
	}

	private void CleanPreviousFurniture() {
		GameObject[] selectFurnitures = GameObject.FindGameObjectsWithTag("select");
		foreach(GameObject selectFurniture in selectFurnitures){
			Destroy(selectFurniture);
		}
	}

	private void CleanPreviousArrows() {
		Destroy(GameObject.Find("Arrows"));
	}

	public void HideMenu() {
		CleanPreviousArrows();
		CleanPreviousFurniture();
	}
}
