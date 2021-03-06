﻿using UnityEngine;
using System.Collections;

public class LoadEditor : MonoBehaviour {

	private EditorGameManager em;

	//used awake so that available furniture is loaded before the editor scene
	void Awake () {
		em = EditorGameManager.Instance;
		LoadAvailableFurnituresFromDB();
	}

	private void LoadAvailableFurnituresFromDB() {
		DBHandler dbh = DBHandler.Instance;
		Furniture[] furnitures = dbh.GetFurnitures();
		em.Furnitures = furnitures;
	}
}
