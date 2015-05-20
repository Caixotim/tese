using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BudgetOverlay : MonoBehaviour {

	private EditorGameManager egm;
	
	void Start () {
		egm = EditorGameManager.Instance;
	}
	
	
	void Update () {
		Text title_txt = GameObject.Find("Overlay_Budget/Item_block/Panel/Budget").GetComponent<Text> ();
		title_txt.text = egm.Budget + " €";
	}
}
