using UnityEngine;
using System.Collections;

public class DeleteTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider coll) {
		if(coll.gameObject.tag == "furniture") {
			FurnitureHandler fh = coll.gameObject.GetComponent<FurnitureHandler>();
			fh.DropFurniture();
			Destroy(coll.gameObject);
		}
	}
}
