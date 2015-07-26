using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {

	private EditorGameManager egm;
	private MenuManager mm;
	private int selectedFurnitureIndex;

	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		selectedFurnitureIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (egm.CurrentWiimote.GetKeyPress((int) Wiimote.KeyCode.BUTTON_A)) {
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
	}

	private void ShowSelectionMenu (bool isToShow) {
		if (isToShow) {
			//Show selection menu
			//Furniture
			GameObject furniture = (GameObject) Resources.Load("Furniture/" + (selectedFurnitureIndex + 1) + "/" + 	(selectedFurnitureIndex + 1), typeof(GameObject));
			mm.ActivatedMenu = MenuManager.Menu.furniture_select;
		} else {
			//Hide selection menu
			mm.ActivatedMenu = MenuManager.Menu.none;
		}
	}
}
