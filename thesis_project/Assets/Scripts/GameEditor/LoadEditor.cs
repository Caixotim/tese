<<<<<<< Updated upstream
﻿using UnityEngine;using System.Collections;public class LoadEditor : MonoBehaviour {	private EditorGameManager em;	//used awake so that available furniture is loaded before the editor scene	void Awake () {		em = EditorGameManager.Instance;		LoadAvailableFurnitures();	}	private void LoadAvailableFurnitures() {		DBHandler dbh = DBHandler.Instance;		Furniture[] furnitures = dbh.GetFurnitures();		em.Furnitures = furnitures;	}}
=======
﻿using UnityEngine;
using System.Collections;

public class LoadEditor : MonoBehaviour {

	private EditorGameManager em;
	private SimulationManager sm;

	//used awake so that available furniture is loaded before the editor scene
	void Awake () {
		em = EditorGameManager.Instance;
		sm = SimulationManager.Instance;
		LoadAvailableFurnituresFromDB();
	}

	void Start () {
		GameObject player = GameObject.Find ("player");
		player.transform.localPosition = new Vector3(player.transform.localPosition.x, sm.UserHeight/100.0f, player.transform.localPosition.z);
	}

	private void LoadAvailableFurnituresFromDB() {
		DBHandler dbh = DBHandler.Instance;
		Furniture[] furnitures = dbh.GetFurnitures();
		em.Furnitures = furnitures;
	}
}
>>>>>>> Stashed changes
