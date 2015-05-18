using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour {
	private EditorGameManager egm;
	private MenuManager mm;

	void Start() {
		egm = EditorGameManager.Instance;
		mm = MenuManager.Instance;
	}

	void Update() {
		if (mm.ActivatedMenu == MenuManager.Menu.none) {
			if (egm.GrabbedObject != null) {
				foreach (GameObject wall in GameObject.FindGameObjectsWithTag("wall")) {
					Renderer rend = wall.GetComponent<Renderer> ();
					rend.material.shader = Shader.Find ("Particles/Additive (Soft)");
					wall.GetComponent<BoxCollider> ().enabled = false;
				}
			} else {
				foreach (GameObject wall in GameObject.FindGameObjectsWithTag("wall")) {
					Renderer rend = wall.GetComponent<Renderer> ();
					rend.material.shader = Shader.Find ("Standard");
					wall.GetComponent<BoxCollider> ().enabled = true;
				}
			}
		}
	}
}
