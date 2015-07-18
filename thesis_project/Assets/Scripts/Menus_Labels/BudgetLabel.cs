using UnityEngine;
using System.Collections;

public class BudgetLabel : MonoBehaviour {

	private SimulationManager sm;
	private string prefixText = "Orçamento: ";

	void Start () {
		sm = SimulationManager.Instance;
	}
	
	void Update () {
		UpdateBudgetLabel();
	}

	private void UpdateBudgetLabel() {
		this.GetComponent<TextMesh>().text = prefixText + sm.Budget + "€";
	}
}
