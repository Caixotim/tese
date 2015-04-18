using UnityEngine;
using System.Collections;

public class FurnitureHandler : MonoBehaviour {

	private Furniture furniture;
	private bool isGrabbed = false;

	//Temporary
	void Start () {
		EditorGameManager egm = EditorGameManager.Instance;
		Furniture furniture_temp = egm.Furnitures [0];
		SetFurniture(furniture_temp);
	}

	//must be the first call to this script
	public void SetFurniture (Furniture _furniture_) {
		furniture = _furniture_;
	}

	public Transform GrabFurniture() {
		Debug.Log("Grab object "+furniture.Name);

		isGrabbed = true;

		this.gameObject.transform.GetComponent<Rigidbody>().useGravity=false;
		this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

		return gameObject.transform;
	}

	public bool DropFurniture () {
		Debug.Log("Drop Object "+furniture.Name);

		//check furniture type to check where furniture can be dropped or attached to
		//if(furniture.Type)....

		this.gameObject.GetComponent<Rigidbody>().useGravity=true;
		this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

		isGrabbed = false;

		return true;
	}

	void Update() {
		if(isGrabbed) {
			this.gameObject.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
			this.gameObject.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}
}
