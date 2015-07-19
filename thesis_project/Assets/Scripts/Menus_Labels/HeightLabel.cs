using UnityEngine;
using System.Collections;

public class HeightLabel : MonoBehaviour {

	private EditorGameManager egm;
	private SimulationManager sm;
	
	void Start () {
		egm = EditorGameManager.Instance;
		sm = SimulationManager.Instance;
	}
	
	void Update () {
		UpdateHeightLabel();	
	}

	private void UpdateHeightLabel() {
		string height = "";
		// if (egm.GrabbedObject != null && egm.GrabbedObject.transform.name.Equals("height_pointer")) {
			height = sm.UserHeight + " cm";
		// }

		this.GetComponent<TextMesh>().text = height;
	}
}
