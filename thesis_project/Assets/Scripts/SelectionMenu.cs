using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour {

	private EditorGameManager egm;
	private MenuManager mm;
	private GameObject player;
	private GameObject selectionMenu;


	public void Start()
	{
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		//find player gameobject
		player = GameObject.Find("OVRPlayerController");
		selectionMenu = GameObject.Find ("SelectionMenu");
	}

	public void DisplaySelectionMenu(bool isToDisplay)
	{
		//Select Model's Menu
		selectionMenu.SetActive (isToDisplay);
	}

	public void NextFurniture() {
		int currentIndex = mm.GetActiveFurnitureIndex ();
		if (currentIndex < egm.Furnitures.Length) {
			mm.SetActiveFurnitureIndex (currentIndex++);
		} else {
			mm.SetActiveFurnitureIndex(0);
		}
		UpdateFurnitureInfo ();
	}

	public void PreviousFurniture() {
		int currentIndex = mm.GetActiveFurnitureIndex ();
		if (currentIndex > 0) {
			mm.SetActiveFurnitureIndex (currentIndex++);
		} else {
			mm.SetActiveFurnitureIndex(0);
		}
		UpdateFurnitureInfo ();
	}

	private void UpdateFurnitureInfo() {
		//set all info on menu
	}
}
