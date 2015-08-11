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

	void Start() {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
		sm = SimulationManager.Instance;

		player = GameObject.Find("Player").transform;
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
		GameObject trigger = GameObject.Find("HeightMenu_Trigger");

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
		GameObject rightArrow = GameObject.Find("UpArrow");
		GameObject leftArrow = GameObject.Find("DownArrow");

		if (rightArrow == null && leftArrow == null) {
			GameObject arrows = (GameObject) Resources.Load("Arrows/UpDownArrows", typeof(GameObject)); //change to height arrows
			GameObject instantiatedArrows = (GameObject) Instantiate(arrows, centerPosition, Quaternion.identity);
			instantiatedArrows.transform.name = "UpDownArrows";
			instantiatedArrows.transform.LookAt(player);
			instantiatedArrows.transform.forward = -instantiatedArrows.transform.forward;

			/*GameObject.Find("UpArrow/Text").GetComponent<TextMesh>().text = upArrowSymbol.ToString();
			GameObject.Find("DownArrow/Text").GetComponent<TextMesh>().text = downArrowSymbol.ToString();*/
		}
	}

	private void DrawLabel (Vector3 centerPosition) {
		Vector3 labelPos = centerPosition;
		GameObject heightLabel = (GameObject) Resources.Load("Menus/Height/Height_Label", typeof(GameObject));
		GameObject instantiatedLabel = (GameObject) Instantiate(heightLabel, labelPos, Quaternion.identity);
		instantiatedLabel.transform.name = "Height_Label";
		instantiatedLabel.transform.forward = -player.transform.forward;
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
