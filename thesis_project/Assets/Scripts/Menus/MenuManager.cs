using System.Collections;

public class MenuManager{

	private static MenuManager _instance = null;
	private bool isOnMenu = false;
	public enum Menu {user_preferences, furniture_select, furniture_properties, none};
	private static Menu activatedMenu = Menu.none;


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

	public bool IsOnMenu
	{
		get
		{
			return isOnMenu;
		}
		set
		{
			isOnMenu = value;
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
