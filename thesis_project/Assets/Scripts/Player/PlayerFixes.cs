using UnityEngine;
using System.Collections;

public class PlayerFixes : MonoBehaviour {

	private SimulationManager sm;

	void Start () {
		sm = SimulationManager.Instance;
	}
	
	void Update () {
		LevelPlayerHeight();
		ResetPlayerVelocity();
	}

	private void LevelPlayerHeight () {
		RaycastHit hit;
		Ray ray = new Ray ();
		ray.origin = this.transform.position;
		ray.direction = -Vector3.up;
		if (Physics.Raycast (ray, out hit, 2.0f)) {
			 if(hit.transform.tag.Equals ("floor")) {

			 	Transform floorObj = hit.transform;
				this.transform.position = new Vector3 (this.transform.position.x, hit.transform.position.y + (sm.UserHeight / 100.0f), this.transform.position.z);
			}
		}
	}

	private void ResetPlayerVelocity () {
		this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
	}
}