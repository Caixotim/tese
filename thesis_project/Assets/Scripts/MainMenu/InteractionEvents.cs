using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionEvents : MonoBehaviour {

	private SimulationManager sm;
	private int? userHeight = null;

	// Use this for initialization
	void Start () {
		sm = SimulationManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartSimulation_ClickEvent () {
		Application.LoadLevel("simulation");
	}

	public void Preferences_ClickEvent () {
		Application.LoadLevel("preferences");
	}

	public void Back_ClickEvent () {
		Application.LoadLevel("mainMenu");
	}

	public void ChangeUserHeight (InputField heightInput) {
		if(heightInput.text.Length == 3) {
			int height = int.Parse(heightInput.text);
			if(height >= 1.0f && height <= 2.9f) {
				userHeight = int.Parse(heightInput.text);
			}
			else {
				//invalid user input feedback
			}
		}
	}

	public void SaveUserHeight() {
		if (userHeight != null) {
			sm.UserHeight = (int)userHeight; 
			Debug.Log(sm.UserHeight);
		}
	}

	public void Quit() {
		Application.Quit ();
	}
}
