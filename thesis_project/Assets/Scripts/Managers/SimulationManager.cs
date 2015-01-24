﻿using System.Collections;

public class SimulationManager{

	private static SimulationManager _instance = null;

	//parameters to load simulation
	private string mapPath;

	//user preferences
	private static int userHeight;

	private SimulationManager(){
		//set default values
		userHeight = 170;
		mapPath = ""; //empty map
	}	

	public static SimulationManager Instance {
		get {
			if (_instance == null) {
				_instance = new SimulationManager();
			}
			return _instance;
		}
	}

	public int UserHeight {
		get {
			return userHeight;
		}
		set {
			userHeight = value;
		}
	}

	public string MapPath {
		get {
			return mapPath;
		}
		set {
			mapPath = value;
		}
	}


}
