using UnityEngine;
using System.Collections;

public class GarbageTrigger : MonoBehaviour {
	
	void OnTriggerExit(Collider other) {
    	if (other.gameObject.tag.Equals("furniture")) {
    		//create garbage can
    	}
    }
}
