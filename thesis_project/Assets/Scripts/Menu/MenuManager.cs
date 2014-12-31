using System.Collections;

public class MenuManager{

	private static MenuManager _instance = null;
	private bool isToRenderMenu = false;


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

	public bool IsToRenderMenu
	{
		get
		{
			return isToRenderMenu;
		}
		set
		{
			isToRenderMenu = value;
		}
	}
}
