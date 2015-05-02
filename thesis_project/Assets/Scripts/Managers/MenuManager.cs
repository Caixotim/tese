using System.Collections;

public class MenuManager{

	private static MenuManager _instance = null;
<<<<<<< Updated upstream:thesis_project/Assets/Scripts/Managers/MenuManager.cs
	private bool isOnMenu = false;
	public enum Menu {user_preferences, furniture_select, furniture_properties, none};
	private static Menu activatedMenu = Menu.none;

=======
	private bool isToRenderMenu = false;
	private int activeFurnitureIndex = 0;
>>>>>>> Stashed changes:thesis_project/Assets/Scripts/Menu/MenuManager.cs

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

	public int GetActiveFurnitureIndex() {
		return activeFurnitureIndex;
	}

	public void SetActiveFurnitureIndex(Furniture newActiveFurnitureIndex) {
		activeFurnitureIndex = newActiveFurnitureIndex;
	}
}
