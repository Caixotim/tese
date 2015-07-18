using UnityEngine;
using System.Collections;

public class TriggerFurnitureBase : MonoBehaviour {

	private SimulationManager sm;
	private SelectInteraction selectionManager;

	void Start() {
		sm = SimulationManager.Instance;
		selectionManager = GameObject.Find("InteractionsHandler").GetComponent<SelectInteraction>();
	}

	void OnTriggerExit(Collider other) {
		GameObject enteredGameObject = other.gameObject;
		if(enteredGameObject.tag.Equals("furniture") && enteredGameObject.name.EndsWith("selection")) {
			string[] newName = enteredGameObject.name.Split("_"[0]);
			enteredGameObject.name = newName[0];
			IncreaseBudget(enteredGameObject.GetComponent<FurnitureHandler>().furniture.Price);

			selectionManager.CreateFurniture();
		}
	}

	private void IncreaseBudget(float price) {
		sm.Budget += price;
	}
}
