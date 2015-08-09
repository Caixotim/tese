using UnityEngine;
using System.Collections;

public class DeleteTrigger : MonoBehaviour {

	private SimulationManager sm;

	void Start() {
		sm = SimulationManager.Instance;
	}

	void OnTriggerEnter(Collider other) {
		//Update budget

		if(other.gameObject.tag == "furniture") {
			FurnitureHandler fh = other.gameObject.GetComponent<FurnitureHandler>();
			DecreaseBudget(fh.furniture.Price);
			fh.DropFurniture();
			Destroy(other.gameObject);
			Destroy(GameObject.Find("Delete"));
		}
    }

    void DecreaseBudget(float price) {
    	sm.Budget -= price;
    }
}
