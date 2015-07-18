﻿using UnityEngine;
using System.Collections;

public class SelectInteraction : MonoBehaviour {

	private EditorGameManager egm;
	private Vector3 modelInitialPos = new Vector3(0.74f, 2.0f, 7.28f);
	private Quaternion modelInitialRotation = Quaternion.identity;
	private int selectedFurnitureIndex = 0;
	private string furniturePath = "Assets/Resources/Furniture/Models/";
	private TextMesh furnitureNameTextMesh;

	public void Start () {
		egm = EditorGameManager.Instance;
		furnitureNameTextMesh = GameObject.Find("Selection_Box/Furniture_Name").GetComponent<TextMesh>();
		CreateFurniture();
	}

	public void ToggleAction (string objectName) {
		switch(objectName) {
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
			DestroyImmediate(GameObject.Find((egm.Furnitures.Length - 1) + "_selection"), false);
		} else {
			DestroyImmediate(GameObject.Find((selectedFurnitureIndex - 1) + "_selection"), false);
		}
		CreateFurniture();
	}

	private void ToggleSelectLeft () {
		//DELETE previous model
		//next model from list (if on last move to first)
		//create new model on "modelInitialPos"
		selectedFurnitureIndex--;
		if (selectedFurnitureIndex < 0) {
			selectedFurnitureIndex = egm.Furnitures.Length - 1;
			DestroyImmediate(GameObject.Find(0 + "_selection"), false);
		} else {
			DestroyImmediate(GameObject.Find((selectedFurnitureIndex + 1) + "_selection"), false);
		}
		CreateFurniture();
	}

	public void CreateFurniture() {
		//Find furniture in resources folder
		string furnitureItem = "" + (selectedFurnitureIndex + 1);
		string fullPath = furniturePath + furnitureItem + "/" + furnitureItem + ".prefab";

		GameObject furniture = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath(fullPath, typeof(GameObject));
		GameObject createdFurniture = (GameObject) Instantiate (furniture, modelInitialPos, modelInitialRotation);
		createdFurniture.name = "" + selectedFurnitureIndex + "_selection";
		FurnitureHandler fh = createdFurniture.GetComponent<FurnitureHandler> ();
		fh.SetFurniture (egm.Furnitures [selectedFurnitureIndex]);

		UpdateFurnitureNameLabel(fh.furniture.Name);
	}

	private void UpdateFurnitureNameLabel(string furnitureName) {
		furnitureNameTextMesh.text = furnitureName;
	}
}
