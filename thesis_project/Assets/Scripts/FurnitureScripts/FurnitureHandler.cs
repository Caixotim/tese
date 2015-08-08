using UnityEngine;using System.Collections;public class FurnitureHandler : MonoBehaviour {		private Furniture furniture;	private EditorGameManager egm;		void Start () {		egm = EditorGameManager.Instance;		//Temporary		Furniture furniture_temp = egm.Furnitures [0];		SetFurniture(furniture_temp);		this.gameObject.GetComponent<Rigidbody> ().mass = furniture.Mass;	}		//must be the first call to this script	public void SetFurniture (Furniture _furniture_) {		furniture = _furniture_;	}		public void GrabFurniture(Transform newParent) {//		Debug.Log("Grab object "+furniture.Name);				this.transform.SetParent (newParent);				egm.IsGrabbingObject = true;		egm.GrabbedObject = this.gameObject;				this.gameObject.transform.GetComponent<Rigidbody>().useGravity=false;		this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;	}		public void DropFurniture () {//		Debug.Log("Drop Object "+furniture.Name);				//		check furniture type to check where furniture can be dropped or attached to		//		if(furniture.Type)....				this.transform.SetParent (null);				this.gameObject.GetComponent<Rigidbody>().useGravity=true;		this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;				egm.IsGrabbingObject = false;		egm.GrabbedObject = null;	}		void Update() {		// if (egm.GrabbedObject == this.gameObject) {			this.gameObject.transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;			this.gameObject.transform.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;		// }	}}