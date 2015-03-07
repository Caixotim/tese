using UnityEngine;
using System.Collections;

public class GrabDropFurniture : MonoBehaviour {

	private bool isGrabbed = false;

	public Transform GrabFurniture() {
		Debug.Log("Grab object");

		isGrabbed = true;

		this.gameObject.transform.rigidbody.useGravity=false;
		this.gameObject.transform.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

		return gameObject.transform;
	}

	public bool DropFurniture () {
		Debug.Log("Drop Object");

		//check furniture type
		this.gameObject.rigidbody.useGravity=true;
		this.gameObject.rigidbody.constraints = RigidbodyConstraints.None;

		isGrabbed = false;

		return true;
	}

	void Update() {
		if(isGrabbed) {
			this.gameObject.transform.rigidbody.velocity = Vector3.zero;
			this.gameObject.transform.rigidbody.angularVelocity = Vector3.zero;
		}
	}
}
