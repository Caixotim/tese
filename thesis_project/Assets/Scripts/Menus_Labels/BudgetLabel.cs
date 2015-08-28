using UnityEngine;
using System.Collections;

public class BudgetLabel : MonoBehaviour {

	private SimulationManager sm;
	private string prefixText = "Total: ";

	void Start () {
		sm = SimulationManager.Instance;
	}
	
	void Update () {
		UpdateBudgetLabel();
	}

	private void UpdateBudgetLabel() {
		this.GetComponent<TextMesh>().text = prefixText + "\n" + sm.Budget + "€";
	}
}
