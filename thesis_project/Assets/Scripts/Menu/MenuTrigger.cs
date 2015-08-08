using UnityEngine;
using System.Collections;

public class MenuTrigger : MonoBehaviour {

	private MenuManager mm;

	void Start () {
		mm = MenuManager.Instance;
	}
	
	void OnTriggerEnter(Collider other) {
        mm.CanDrawMenu = false;
    }

    void OnTriggerExit(Collider other) {
    	mm.CanDrawMenu = true;
    }
}
