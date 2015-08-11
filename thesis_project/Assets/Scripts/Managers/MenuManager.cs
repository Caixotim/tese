using System.Collections;

public class MenuManager {

	private static MenuManager _instance = null;
	public enum Menu {user_preferences, furniture_select, none};
	private static Menu activatedMenu = Menu.none;
	private static bool canDrawMenu = true;
	private static bool canDrawHeightMenu = true;

	private MenuManager() {

	}

	public static MenuManager Instance {
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

	public bool CanDrawMenu {
		get {
			return canDrawMenu;
		}
		set {
			canDrawMenu = value;
		}
	}

	public bool CanDrawHeightMenu {
		get {
			return canDrawHeightMenu;
		}
		set {
			canDrawHeightMenu = value;
		}
	}
}