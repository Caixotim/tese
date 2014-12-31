using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {

	private EditorGameManager egm;

	public void Start()
	{
		egm = EditorGameManager.Instance;

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

	}

	private void DeactivateSelectionMenu()
	{

	}
}
