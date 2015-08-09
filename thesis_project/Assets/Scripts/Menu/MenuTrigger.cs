using UnityEngine;
using System.Collections;

public class MenuTrigger : MonoBehaviour {

	private MenuManager mm;
	private bool triggered = false;
	private Collider other;

	void Start () {
		mm = MenuManager.Instance;
	}
	
	void OnTriggerEnter(Collider other) {
		triggered = true;
        mm.CanDrawMenu = false;
        this.other = other;
    }

    void OnTriggerExit(Collider other) {
    	mm.CanDrawMenu = true;
    }

    void Update() {
    	if (triggered && !other) {
    		mm.CanDrawMenu = true;
    	}
    }
}
