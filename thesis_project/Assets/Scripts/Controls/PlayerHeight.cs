using UnityEngine;
using System.Collections;

public class PlayerHeight : MonoBehaviour {

	private SimulationManager sm;

	void Start () {
		sm = SimulationManager.Instance;
	}
	
	void Update () {
		SetUserHeight();
	}

	private void SetUserHeight() {
		RaycastHit hit;
		if(Physics.Raycast(transform.position, -Vector3.up, out hit, 10)) {
			Vector3 newPosition;
			if(!hit.transform.name.Equals("Floor")) {
				float objectHalfHeight = hit.transform.GetComponent<Renderer>().bounds.extents.y;
				newPosition = new Vector3(this.gameObject.transform.position.x, sm.UserHeight/100.0f + objectHalfHeight, this.gameObject.transform.position.z);
			} else {
				newPosition = new Vector3(this.gameObject.transform.position.x, sm.UserHeight/100.0f, this.gameObject.transform.position.z);
			}
			this.gameObject.transform.position = newPosition;
		}
	}
}
