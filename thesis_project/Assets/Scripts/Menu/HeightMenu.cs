using UnityEngine;
using System.Collections;

public class HeightMenu : MonoBehaviour {

	private EditorGameManager egm;
	private MenuManager mm;
	private SimulationManager sm;
	private bool drawnMenu = false;
	private int deltaHeight = 1;
	private Transform player;

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
			case "RightArrow":
				IncreaseHeight();
				break;
			case "LeftArrow":
				DecreaseHeight();
				break;
		}
	}

	private void IncreaseHeight() {
		if(sm.UserHeight < 2.5f) {
			sm.UserHeight += deltaHeight;
			DrawHeightMenu(true);
		}
	}

	private void DecreaseHeight() {
		if(sm.UserHeight < 1.0f) {
			sm.UserHeight -= deltaHeight;
			DrawHeightMenu(true);
		}
	}

	private void ShowHeightMenu (bool isToShow) {
		if (isToShow && !drawnMenu) {
			if (mm.CanDrawMenu) {
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
		GameObject trigger = GameObject.Find("Menu_Trigger");

		if (toDraw) {
			Vector3 heightMenuPos;

			heightMenuPos = trigger.transform.position;

			//save previous label direction
			GameObject label = GameObject.Find("Furniture_Label"); //Change to just change text
			Vector3 direction = Vector3.zero;
			if (label != null) {
				direction = label.transform.forward;
			}

			CleanPreviousLabel();

			DrawArrows(heightMenuPos);
			DrawLabel(heightMenuPos, direction);
			
			mm.ActivatedMenu = MenuManager.Menu.user_preferences;
			drawnMenu = true;
		} else {
			//hide selection menu
			HideMenu();
			drawnMenu = false;
		}
	}

	private void DrawArrows (Vector3 centerPosition) {
		GameObject rightArrow = GameObject.Find("RightArrow");
		GameObject leftArrow = GameObject.Find("LeftArrow");

		if (rightArrow == null && leftArrow == null) {
			GameObject arrows = (GameObject) Resources.Load("Arrows/Arrows", typeof(GameObject)); //change to height arrows
			GameObject instantiatedArrows = (GameObject) Instantiate(arrows, centerPosition, Quaternion.identity);
			instantiatedArrows.transform.name = "Height_Menu_Arrows";
			instantiatedArrows.transform.LookAt(player);
			instantiatedArrows.transform.forward = -instantiatedArrows.transform.forward;
		}
	}

	private void DrawLabel (Vector3 centerPosition, Vector3 direction) {
		Vector3 labelPos = centerPosition;
		GameObject heightLabel = (GameObject) Resources.Load("Menus/Height/Height_Label", typeof(GameObject));
		GameObject instantiatedLabel = (GameObject) Instantiate(heightLabel, labelPos, Quaternion.identity);
		instantiatedLabel.transform.name = "Height_Label";
		instantiatedLabel.transform.forward = direction;
		instantiatedLabel.transform.GetComponent<TextMesh>().text = sm.UserHeight + " cm";
	}

	private void CleanPreviousArrows() {
		Destroy(GameObject.Find("Height_Menu_Arrows"));
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
