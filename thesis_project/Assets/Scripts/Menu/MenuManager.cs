﻿using System.Collections;

public class MenuManager{

	private static MenuManager _instance = null;
	public enum Menu {user_preferences, furniture_select, furniture_properties, none};
	private static Menu activatedMenu = Menu.user_preferences;


	private MenuManager()
	{
	}

	public static MenuManager Instance
	{
		get
		{
			if(_instance == null)
				_instance = new MenuManager();
			return _instance;
		}
	}

	public Menu ActivatedMenu {
		get {
			return activatedMenu;
		}
		set {
			activatedMenu = value;
		}
	}
}
