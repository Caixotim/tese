using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {

	private EditorGameManager egm;
	private MenuManager mm;
	private int selectedFurnitureIndex;
	private float distanceToFurnitureSelect = 0.5f;

	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		selectedFurnitureIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (egm.CurrentWiimote.GetKeyPress((int) Wiimote.KeyCode.NUNCHUCK_C)) {
			ShowSelectionMenu(mm.ActivatedMenu == MenuManager.Menu.none);
		}
	}

	void DoAction () {
		if (this.gameObject.name.Equals("Right")) {
			SelectRight();
		} else if(this.gameObject.name.Equals("Left")) {
			SelectLeft();
		}
		ShowSelectionMenu(true);
	}

	private void SelectRight() {
		if (selectedFurnitureIndex < egm.Furnitures.Length - 1) {
			selectedFurnitureIndex++;
		} else {
			selectedFurnitureIndex = 0;
		}
		ShowSelectionMenu(true);
	}

	private void SelectLeft() {
		if (selectedFurnitureIndex > 0) {
			selectedFurnitureIndex--;
		} else {
			selectedFurnitureIndex = egm.Furnitures.Length - 1;
		}
		ShowSelectionMenu(true);
	}

	private void ShowSelectionMenu (bool isToShow) {
		if (isToShow) {
			//Show selection menu
			DrawSelectionMenu(true);
		} else if (mm.ActivatedMenu == MenuManager.Menu.furniture_select) {
			//Hide selection menu
			DrawSelectionMenu(false);
		}
	}

	private void DrawSelectionMenu(bool toDraw) {
		GameObject trigger = GameObject.Find("Menu_Trigger");

		if (toDraw) {
			//draw selection menu
			GameObject furniture = (GameObject) Resources.Load("Furniture/" + (selectedFurnitureIndex + 1) + "/" + 	(selectedFurnitureIndex + 1), typeof(GameObject));
			furniture.transform.tag = "select";
			Instantiate(furniture, trigger.transform.position, Quaternion.identity);
			DrawArrows(furniture.transform.position);
			
			mm.ActivatedMenu = MenuManager.Menu.furniture_select;
		} else {
			//hide selection menu
			mm.ActivatedMenu = MenuManager.Menu.none;
		}
	}

	private void DrawArrows (Vector3 centerPosition) {
		GameObject rightArrow = GameObject.Find("RightArrow");
		GameObject leftArrow = GameObject.Find("LeftArrow");

		if(rightArrow == null) {
			rightArrow = (GameObject) Resources.Load("Arrows/RightArrow", typeof(GameObject));
			rightArrow.transform.tag = "interactable";
			Instantiate(rightArrow, new Vector3(centerPosition.x + distanceToFurnitureSelect, centerPosition.y, centerPosition.z), Quaternion.identity);
		}

		if(leftArrow == null) {
			leftArrow = (GameObject) Resources.Load("Arrows/LeftArrow", typeof(GameObject));
			leftArrow.transform.tag = "interactable";
			Instantiate(leftArrow, new Vector3(centerPosition.x - distanceToFurnitureSelect, centerPosition.y, centerPosition.z), Quaternion.identity);
		}
	}
}
