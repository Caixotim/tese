using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour {

	private EditorGameManager egm;
	private GameObject player;


	public void Start()
	{
		egm = EditorGameManager.Instance;
		//find player gameobject
		player = GameObject.Find("CenterEyeAnchor");
	}

	public void DisplaySelectionMenu(bool isToDisplay)
	{
		if(isToDisplay){
			//Select Model's Menu
			RenderSelectionMenu();
		}
		else{
			DeactivateSelectionMenu();
		}
	}

	private void RenderSelectionMenu()
	{
		//simple menu
		
	}

	private void DeactivateSelectionMenu()
	{

	}
}
