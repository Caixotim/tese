using UnityEngine;
using System.Collections;

public class TriggerFurnitureBase : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		GameObject enteredGameObject = other.gameObject;
		if(enteredGameObject.tag.Equals("furniture") && enteredGameObject.name.EndsWith("selection")) {
			string[] newName = enteredGameObject.name.Split("_"[0]);
			enteredGameObject.name = newName[0];
		}
	}
}
