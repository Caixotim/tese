using UnityEngine;
using System.Collections;

public class SelectInteraction : MonoBehaviour {

	private EditorGameManager egm;
	private Vector3 modelInitialPos = new Vector3(0.74f, 2.0f, 7.28f);
	private Quaternion modelInitialRotation = Quaternion.identity;
	private int selectedFurnitureIndex = 0;
	private string furniturePath = "/Furniture/Models/";

	public void Start () {
		egm = EditorGameManager.Instance;
		CreateFurniture();
	}

	public void ToggleAction () {
		switch(this.gameObject.name) {
			case "Select_Right":
				ToggleSelectRight();
				break;
			case "Select_Left":
				ToggleSelectLeft();
				break;
		}
	}

	private void ToggleSelectRight () {
		//DELETE previous model
		//next model from list (if on last move to first)
		//create new model on "modelInitialPos"
		selectedFurnitureIndex++;
		if (selectedFurnitureIndex > egm.Furnitures.Length - 1) {
			selectedFurnitureIndex = 0;
			DestroyImmediate(GameObject.Find(egm.Furnitures[egm.Furnitures.Length].Name));
		} else {
			DestroyImmediate(GameObject.Find(egm.Furnitures[selectedFurnitureIndex - 1].Name));
		}

		Debug.Log("Select_Right");
	}

	private void ToggleSelectLeft () {
		//DELETE previous model
		//next model from list (if on last move to first)
		//create new model on "modelInitialPos"
		selectedFurnitureIndex--;
		if (selectedFurnitureIndex < 0) {
			selectedFurnitureIndex = egm.Furnitures.Length - 1;
			DestroyImmediate(GameObject.Find(egm.Furnitures[0].Name));
		} else {
			DestroyImmediate(GameObject.Find(egm.Furnitures[selectedFurnitureIndex].Name));
		}

		Debug.Log("Select_Left");
	}

	private void CreateFurniture() {
		//Find furniture in resources folder
		string furnitureItem = "" + (selectedFurnitureIndex + 1);
		string fullPath = furniturePath + furnitureItem + "/" + furnitureItem;
		GameObject furniture = Resources.Load<GameObject> (fullPath);
		Debug.Log("full path: " + fullPath + "\n funiture: " + furniture);
		furniture.name = "" + selectedFurnitureIndex;

		GameObject createdFurniture = (GameObject) Instantiate (furniture, modelInitialPos, modelInitialRotation);
		FurnitureHandler fh = createdFurniture.GetComponent<FurnitureHandler> ();
		fh.SetFurniture (egm.Furnitures [selectedFurnitureIndex]);
	}
}
