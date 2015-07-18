using UnityEngine;
using System.Collections;

public class FixHeightPointer : MonoBehaviour {

	EditorGameManager egm;
	SimulationManager sm;

	float posX = -9.4f;
	float posZ = 12.9f;

	// Use this for initialization
	void Start () {
		egm = EditorGameManager.Instance;
		sm = SimulationManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("object pos = " + this.transform.position);

		FixPosition();
		SetUserHeight();
	}

	private void FixPosition () {
		float posY = this.transform.position.y;
		if (this.transform.position.y < 0.5f) {
			posY = 0.5f;
		} else if(this.transform.position.y > 2.5f) {
			posY = 2.5f;
		}

		this.transform.position = new Vector3(posX, posY, posZ);
	}

	private void SetUserHeight () {
		sm.UserHeight = (int) (this.transform.position.y * 100.0f);
	}
}
