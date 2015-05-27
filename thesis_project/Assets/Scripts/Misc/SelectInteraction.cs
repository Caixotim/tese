using UnityEngine;
using System.Collections;

public class SelectInteraction : MonoBehaviour {

	private Vector3 modelInitialPos = new Vector3(0, 0, 0);

	public void Start () {

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
		Debug.Log("Select_Right");
	}

	private void ToggleSelectLeft () {
		//DELETE previous model
		//next model from list (if on last move to first)
		//create new model on "modelInitialPos"
		Debug.Log("Select_Left");
	}

}
