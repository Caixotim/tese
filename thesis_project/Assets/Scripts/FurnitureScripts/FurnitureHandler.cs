using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FurnitureHandler : MonoBehaviour {

	private Furniture furniture;
	private EditorGameManager egm;
	private MenuManager mm;
	private float furnitureHeight;
	private List<GameObject> objectsAttached = new List<GameObject>();
	
	void Start () {
		furnitureHeight = this.gameObject.GetComponent<BoxCollider>().bounds.size.y;
	}

	void Awake () {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		//Temporary
		/*Furniture furniture_temp = egm.Furnitures [0];
		FurnitureInfo(furniture_temp);
		this.gameObject.GetComponent<Rigidbody> ().mass = furniture.Mass;*/
	}

	//must be the first call to this script
	public Furniture FurnitureInfo{
		get {
			return furniture;
		}
		set {
			furniture = value;
			this.gameObject.GetComponent<Rigidbody> ().mass = furniture.Mass;
		}
	}

	public void GrabFurniture(Transform newParent) {
		this.transform.SetParent (newParent);

		egm.GrabbedObject = this.gameObject;
		
		GrabObjectsOnTop();

		this.gameObject.transform.GetComponent<Rigidbody>().useGravity=false;
		this.gameObject.transform.GetComponent<Rigidbody>().detectCollisions = false;

		this.gameObject.transform.GetComponent<BoxCollider>().isTrigger = true;

		mm.ActivatedMenu = MenuManager.Menu.furniture_properties;
		Debug.Log("grab active menu: " + mm.ActivatedMenu);
	}

	public void DropFurniture () {
//		check furniture type to check where furniture can be dropped or attached to
//		if(furniture.Type)....

		this.gameObject.GetComponent<Rigidbody>().useGravity=true;
		this.gameObject.transform.GetComponent<Rigidbody>().detectCollisions = true;

		this.gameObject.transform.GetComponent<BoxCollider>().isTrigger = false;

		this.transform.SetParent (null);

		egm.IsGrabbingObject = false;
		egm.GrabbedObject = null;
	}

	public void DeleteFurniture () {
		egm.Budget = egm.Budget - furniture.Price;
		
		this.transform.SetParent (null);
		egm.GrabbedObject = null;

		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		Vector3 newPosition = Vector3.zero;
		// Debug.Log("Enter");
		if(other.transform.name == "Floor") {
			newPosition = new Vector3(transform.position.x, (furnitureHeight/2.0f), transform.position.z);
		} else if(other.transform.tag == "furniture") {
			float bottomObjectPosY = other.transform.position.y;
			float objectHeight = other.gameObject.GetComponent<BoxCollider>().bounds.size.y;
			newPosition = new Vector3(transform.position.x, bottomObjectPosY + (furnitureHeight/2.0f) + (objectHeight/2.0f), transform.position.z);
		}
		this.gameObject.transform.position = newPosition;
	}

	void Update() {
		if (egm.GrabbedObject == this.gameObject) {
			this.gameObject.transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			this.gameObject.transform.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			AdjustHeight();
		}
		//Fix so that object does not rotate
		this.gameObject.transform.up = Vector3.up;
	}

	private void AdjustHeight() {
		if(this.gameObject.transform.parent != null || egm.GrabbedObject != null) {
			RaycastHit hit;
			Vector3 newPosition = Vector3.zero;
			if (Physics.Raycast(transform.position, -Vector3.up, out hit)) {
				if(hit.transform.tag.Equals("floor")) {
					// Debug.Log("AdjustHeight - Floor");
					newPosition = new Vector3(this.transform.position.x, furnitureHeight/2.0f, this.transform.position.z);
				} else if(hit.transform.tag.Equals("furniture")) {
					// Debug.Log("AdjustHeight - Furniture");
					float bottomObjectPosY = hit.transform.position.y;
					float bottomObjectHeight = (float) hit.transform.gameObject.GetComponent<BoxCollider>().bounds.size.y;
					newPosition = new Vector3(this.transform.position.x, bottomObjectPosY + (furnitureHeight/2.0f) + (bottomObjectHeight/2.0f), this.transform.position.z);
				}
				this.transform.position = newPosition;
			}
		}
	}

	public void GrabObjectsOnTop () {
		RaycastHit[] hits;
		hits = Physics.RaycastAll(this.gameObject.transform.position, this.gameObject.transform.up, 50.0f);

		// Debug.Log(this.gameObject.transform.name);
		foreach (RaycastHit topObj in hits) {
			Debug.Log("hit: " + topObj.transform.name);
			if (topObj.transform.tag.Equals("furniture")) {
				objectsAttached.Add(topObj.transform.gameObject);
				topObj.transform.SetParent(this.gameObject.transform);
			}
		}
		Debug.Log("Grabbed " + objectsAttached.Count + " objects on top");
	}

	public void DropObjectsBellow () {
		foreach (GameObject objectAttached in objectsAttached) {
			objectAttached.transform.SetParent(null);
		}
		objectsAttached.Clear();
	}

}
