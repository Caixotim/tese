using UnityEngine;using System.Collections;public class FurnitureHandler : MonoBehaviour {		public Furniture furniture;	private EditorGameManager egm;		void Start () {		egm = EditorGameManager.Instance;	}	//must be the first call to this script	public void SetFurniture (Furniture _furniture_) {		furniture = _furniture_;		this.gameObject.GetComponent<Rigidbody> ().mass = furniture.Mass;	}		public void GrabFurniture(Transform newParent) {		this.transform.SetParent (newParent);				egm.IsGrabbingObject = true;		egm.GrabbedObject = this.gameObject;				this.gameObject.transform.GetComponent<Rigidbody>().useGravity=false;		this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;	}		public void DropFurniture () {		//		check furniture type to check where furniture can be dropped or attached to		//		if(furniture.Type)....				this.transform.SetParent (null);				this.gameObject.GetComponent<Rigidbody>().useGravity=true;		this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;				egm.IsGrabbingObject = false;		egm.GrabbedObject = null;	}		//	void onCollisionEnter(Collision collision) {	//		collision.gameObject.transform.parent = null; //drop object on collision	//	}		void Update() {		if (egm.GrabbedObject == this.gameObject) {			this.gameObject.transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;			this.gameObject.transform.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;		}	}}