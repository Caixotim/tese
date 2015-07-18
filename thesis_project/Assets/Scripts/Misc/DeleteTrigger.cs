using UnityEngine;
using System.Collections;

public class DeleteTrigger : MonoBehaviour {

	private SimulationManager sm;

	void Start() {
		sm = SimulationManager.Instance;
	}

	void OnTriggerEnter(Collider coll) {
		if(coll.gameObject.tag == "furniture") {
			FurnitureHandler fh = coll.gameObject.GetComponent<FurnitureHandler>();
			DecreaseBudget(fh.furniture.Price);
			fh.DropFurniture();
			Destroy(coll.gameObject);
		}
	}

	private void DecreaseBudget(float price) {
		sm.Budget -= price;
	}
}
