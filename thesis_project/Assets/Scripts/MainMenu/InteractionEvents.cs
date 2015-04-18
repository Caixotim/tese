using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionEvents : MonoBehaviour {

	private SimulationManager sm;

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

	public void LoadMap_ClickEvent (Button LoadMapButton) {
		string[] loadMapText = new string[2];
		string mapToLoad = "";//EditorUtility.OpenFilePanel ("Load Map" , Application.dataPath, ".map");
		if(!string.IsNullOrEmpty(mapToLoad)) {
			if(LoadMapButton.GetComponent<GUIText>().text.IndexOf(':') != -1) {
				loadMapText = LoadMapButton.GetComponent<GUIText>().text.Split(':');
				loadMapText[1] = mapToLoad;
				LoadMapButton.GetComponent<GUIText>().text = loadMapText[0] + ":(" + loadMapText[1] + ")";
			}
		}
	}

	public void Back_ClickEvent () {
		Application.LoadLevel("mainMenu");
	}

	public void ChangeUserHeight (InputField heightInput) {
		if(heightInput.text.Length == 3) {
			sm.UserHeight = int.Parse(heightInput.text);
		}
		else {
			sm.UserHeight = 170;
		}
		//Debug.Log(sm.UserHeight);
	}

	public void ChangeController (int index) {
		sm.SelectedController = (SimulationManager.Controller)index;
		Debug.Log(sm.SelectedController);
	}
}
