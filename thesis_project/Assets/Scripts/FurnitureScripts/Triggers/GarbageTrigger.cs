using UnityEngine;
using System.Collections;

public class GarbageTrigger : MonoBehaviour {
	
	void OnTriggerExit(Collider other) {
    	if (other.gameObject.tag.Equals("furniture")) {
    		//create garbage can
    		Vector3 garbagePos = new Vector3 (this.gameObject.transform.position.x, 0.63f, this.gameObject.transform.position.z);
    		GameObject garbageCan = (GameObject) Resources.Load("Garbage/trash", typeof(GameObject));
    		Instantiate(garbageCan, garbagePos , Quaternion.identity);
    	}
    }
}
