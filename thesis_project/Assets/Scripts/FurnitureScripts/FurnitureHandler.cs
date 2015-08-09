using UnityEngine;using System.Collections;public class FurnitureHandler : MonoBehaviour {		public Furniture furniture;	private EditorGameManager egm;	private MenuManager mm;	private SimulationManager sm;		void Start () {		egm = EditorGameManager.Instance;		mm = MenuManager.Instance;		sm = SimulationManager.Instance;		//Temporary		Furniture furniture_temp = egm.Furnitures [int.Parse(this.gameObject.name)];		SetFurniture(furniture_temp);		this.gameObject.GetComponent<Rigidbody> ().mass = furniture.Mass;	}		//must be the first call to this script	public void SetFurniture (Furniture _furniture_) {		furniture = _furniture_;	}		public void GrabFurniture(Transform newParent) {//		Debug.Log("Grab object "+furniture.Name);		if (this.gameObject.transform.tag.Equals("select")) {			this.gameObject.transform.tag = "furniture";			this.gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;			IncreaseBudget(furniture.Price);		}		if (mm.ActivatedMenu == MenuManager.Menu.furniture_select) {			GameObject.Find("Menu_Interaction").GetComponent<SelectionMenu>().HideMenu();		} else if (mm.ActivatedMenu == MenuManager.Menu.none) {			DropDeleteTrigger();		}				this.transform.SetParent (newParent);				egm.IsGrabbingObject = true;		egm.GrabbedObject = this.gameObject;				this.gameObject.transform.GetComponent<Rigidbody>().useGravity = false;		this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;	}		public void DropFurniture () {//		Debug.Log("Drop Object "+furniture.Name);				//		check furniture type to check where furniture can be dropped or attached to		//		if(furniture.Type)....				this.transform.SetParent (null);				this.gameObject.GetComponent<Rigidbody>().useGravity = true;		this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;				egm.IsGrabbingObject = false;		egm.GrabbedObject = null;	}	private void DropDeleteTrigger() {		// GameObject deleteTrigger = (GameObject) Resources.Load("Furniture/" + (selectedFurnitureIndex + 1) + "/" + 	(selectedFurnitureIndex + 1), typeof(GameObject));	}	private void IncreaseBudget(float price) {		sm.Budget += price;	}		void Update() {		if (egm.GrabbedObject == this.gameObject) {			this.gameObject.transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;			this.gameObject.transform.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;		}	}}