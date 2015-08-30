using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {

	private EditorGameManager egm;
	private MenuManager mm;
	private SimulationManager sm;
	private int selectedFurnitureIndex;
	private bool drawnMenu = false;
	private Vector3 furnitureSelectPos = Vector3.zero;
	private Transform player;

	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		sm = SimulationManager.Instance;
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

			//save previous label direction
			GameObject label = GameObject.Find("Furniture_Label");
			Vector3 direction = Vector3.zero;
			if (label != null) {
				direction = label.transform.forward;
			}

			CleanPreviousLabel();

			//draw selection menu
			GameObject furniture = (GameObject) Resources.Load("Furniture/" + (selectedFurnitureIndex + 1) + "/" + 	(selectedFurnitureIndex + 1), typeof(GameObject));
			furniture.transform.tag = "select";

			GameObject instantiatedFurniture = (GameObject) Instantiate(furniture, furniturePos, Quaternion.identity);
			instantiatedFurniture.gameObject.name = "" + (selectedFurnitureIndex + 1);
			furnitureSelectPos = instantiatedFurniture.transform.position;
			instantiatedFurniture.transform.LookAt(player);
			instantiatedFurniture.transform.forward = -instantiatedFurniture.transform.forward;
			DrawArrows(instantiatedFurniture.transform.position);
			DrawFurnitureLabel(instantiatedFurniture, direction);
			
			mm.ActivatedMenu = MenuManager.Menu.furniture_select;
			drawnMenu = true;
		} else {
			//hide selection menu
			HideMenu();
			drawnMenu = false;
		}
	}

	private void DrawArrows (Vector3 centerPosition) {
		GameObject rightArrow = GameObject.Find("RightArrow");
		GameObject leftArrow = GameObject.Find("LeftArrow");

		if (rightArrow == null && leftArrow == null) {
			GameObject arrows = (GameObject) Resources.Load("Arrows/RightLeftArrows", typeof(GameObject));
			GameObject instantiatedArrows = (GameObject) Instantiate(arrows, centerPosition, Quaternion.identity);
			instantiatedArrows.transform.name = "RightLeftArrows";
			instantiatedArrows.transform.LookAt(player);
			instantiatedArrows.transform.forward = -instantiatedArrows.transform.forward;
		}
	}

	private void DrawFurnitureLabel (GameObject furniture, Vector3 direction) {
		Vector3 pos = furniture.transform.position;
		BoxCollider bc = furniture.transform.GetComponent<BoxCollider>();
		float height = bc.bounds.size.y;
		Vector3 labelPos = new Vector3(pos.x, pos.y - height/2.0f - 0.8f, pos.z);
		GameObject furnitureLabel = (GameObject) Resources.Load("Furniture/Label/Furniture_Label", typeof(GameObject));
		GameObject instantiatedLabel = (GameObject) Instantiate(furnitureLabel, labelPos, Quaternion.identity);
		instantiatedLabel.transform.name = "Furniture_Label";
		if (direction == Vector3.zero) {
			instantiatedLabel.transform.LookAt(player);
			instantiatedLabel.transform.forward = -instantiatedLabel.transform.forward;
		} else {
			instantiatedLabel.transform.forward = direction;
		}
		string nameAndPrice = egm.Furnitures[selectedFurnitureIndex].Name + " : " + egm.Furnitures[selectedFurnitureIndex].Price + "€";
		instantiatedLabel.transform.GetComponent<TextMesh>().text = nameAndPrice + "\n" + "Total: " + sm.Budget + "€";
	}

	private void CleanPreviousFurniture() {
		GameObject[] selectFurnitures = GameObject.FindGameObjectsWithTag("select");
		foreach(GameObject selectFurniture in selectFurnitures){
			Destroy(selectFurniture);
		}
	}

	private void CleanPreviousArrows() {
		Destroy(GameObject.Find("RightLeftArrows"));
	}

	private void CleanPreviousLabel() {
		Destroy(GameObject.Find("Furniture_Label"));

	}

	public void HideMenu() {
		CleanPreviousLabel();
		CleanPreviousArrows();
		CleanPreviousFurniture();
		drawnMenu = false;
		mm.ActivatedMenu = MenuManager.Menu.none;
	}
}
