using UnityEngine;
using System.Collections;

public class TriggerName : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		GameObject enteredGameObject = other.gameObject;
		Debug.Log("Exit");
		Debug.Log(enteredGameObject.name);
		if(enteredGameObject.tag.Equals("furniture") && enteredGameObject.name.EndsWith("selection")) {
			string[] newName = enteredGameObject.name.Split("_"[0]);
			enteredGameObject.name = newName[0];
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("entered trigger");
	}

	void OnTriggerStay(Collider other) {
		Debug.Log(other.gameObject.name + " is inside furniture base!");
	}
}
