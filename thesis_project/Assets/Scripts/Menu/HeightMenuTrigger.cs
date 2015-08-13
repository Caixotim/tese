using UnityEngine;
using System.Collections;

public class HeightMenuTrigger : MonoBehaviour {

	private MenuManager mm;
	private bool triggered = false;
	private Collider other;

	void Start () {
		mm = MenuManager.Instance;
	}
	
	void OnTriggerEnter(Collider other) {
		triggered = true;
        mm.CanDrawHeightMenu = false;
        this.other = other;
    }

    void OnTriggerExit(Collider other) {
    	mm.CanDrawHeightMenu = true;
    }

    void Update() {
    	if (triggered && !other) {
    		mm.CanDrawHeightMenu = true;
    	}
    }
}
