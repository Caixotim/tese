using UnityEngine;
using System.Collections;

public class HeightMenu : MonoBehaviour {

	private EditorGameManager egm;
	private MenuManager mm;
	private SimulationManager sm;
	private bool drawnMenu = false;
	private int deltaHeight = 1;
	private Transform player;
	private char upArrowSymbol = '\u25B2';
	private char downArrowSymbol = '\u25BC';
	private GameObject trigger;

	void Start() {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		sm = SimulationManager.Instance;

		player = GameObject.Find("Player").transform;
		trigger = GameObject.Find("HeightMenu_Trigger");
	}

	void Update () {
		if (egm.CurrentWiimote.GetKeyPress((int) Wiimote.KeyCode.NUNCHUCK_Z)) {
			ShowHeightMenu(mm.ActivatedMenu == MenuManager.Menu.none);
		}
	}

	public void DoAction (string direction) {
		switch(direction) {
			case "UpArrow":
				IncreaseHeight();
				break;
			case "DownArrow":
				DecreaseHeight();
				break;
		}
	}

	private void IncreaseHeight() {
		if(sm.UserHeight < 250) {
			sm.UserHeight += deltaHeight;
			DrawHeightMenu(true);
		}
	}

	private void DecreaseHeight() {
		if(sm.UserHeight > 100) {
			sm.UserHeight -= deltaHeight;
			DrawHeightMenu(true);
		}
	}

	private void ShowHeightMenu (bool isToShow) {
		if (isToShow && !drawnMenu) {
			if (mm.CanDrawHeightMenu) {
				//Show selection menu
				DrawHeightMenu(true);
			} else {
				// WarningMessage();
			}
		} else if (mm.ActivatedMenu == MenuManager.Menu.user_preferences) {
			//Hide selection menu
			DrawHeightMenu(false);
		}
	}

	private void DrawHeightMenu(bool toDraw) {
		if (toDraw) {
			Vector3 heightMenuPos;

			heightMenuPos = trigger.transform.position;

			//save previous label direction
			GameObject label = GameObject.Find("Height_Label"); //Change to just change text
			if (label != null) {
				label.GetComponent<TextMesh>().text = sm.UserHeight + " cm";
			} else {
				DrawLabel(heightMenuPos);
			}

			DrawArrows(heightMenuPos);
			
			mm.ActivatedMenu = MenuManager.Menu.user_preferences;
			drawnMenu = true;
		} else {
			//hide selection menu
			HideMenu();
			drawnMenu = false;
		}
	}

	private void DrawArrows (Vector3 centerPosition) {
		GameObject upDownArrows = GameObject.Find("UpDownArrows");

		if (upDownArrows == null) {
			GameObject arrows = (GameObject) Resources.Load("Arrows/UpDownArrows", typeof(GameObject)); //change to height arrows
			GameObject instantiatedArrows = (GameObject) Instantiate(arrows, centerPosition, Quaternion.identity);
			instantiatedArrows.transform.name = "UpDownArrows";

			instantiatedArrows.transform.SetParent(trigger.transform);
			instantiatedArrows.transform.rotation = Quaternion.identity;
			instantiatedArrows.transform.LookAt(player);
			instantiatedArrows.transform.forward = player.transform.forward;
			instantiatedArrows.transform.localPosition = new Vector3(0, -2.2f, 0);
			instantiatedArrows.transform.SetParent(null);
		}
	}

	private void DrawLabel (Vector3 centerPosition) {
		Vector3 labelPos = centerPosition;
		GameObject heightLabel = (GameObject) Resources.Load("Menus/Height/Height_Label", typeof(GameObject));
		GameObject instantiatedLabel = (GameObject) Instantiate(heightLabel, Vector3.zero, Quaternion.identity);
		instantiatedLabel.transform.name = "Height_Label";
		
		instantiatedLabel.transform.SetParent(trigger.transform);
		instantiatedLabel.transform.rotation = Quaternion.identity;
		instantiatedLabel.transform.LookAt(player);
		instantiatedLabel.transform.forward = player.transform.forward;
		instantiatedLabel.transform.localPosition = new Vector3(-0.72f, 0, 0);
		instantiatedLabel.transform.SetParent(null);
		
		instantiatedLabel.transform.GetComponent<TextMesh>().text = sm.UserHeight + " cm";
	}

	private void CleanPreviousArrows() {
		Destroy(GameObject.Find("UpDownArrows"));
	}

	private void CleanPreviousLabel() {
		Destroy(GameObject.Find("Height_Label"));
	}

	public void HideMenu() {
		CleanPreviousArrows();
		CleanPreviousLabel();
		drawnMenu = false;
		mm.ActivatedMenu = MenuManager.Menu.none;
	}
}
