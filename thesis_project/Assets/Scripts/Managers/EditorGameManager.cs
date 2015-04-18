
public class EditorGameManager{

	private static EditorGameManager _instance;
	private Wiimote detectedWiiMote;
	private bool disableMenus;
	private bool activateSelectionMenu;
	private Furniture[] furnitures;
	
	private EditorGameManager()
	{
		detectedWiiMote = null;
	}

	public static EditorGameManager Instance 
	{
		get 
		{
			if (_instance == null)
				_instance = new EditorGameManager ();
			return _instance;
		}
	}

	//Detected Wiimote
	public Wiimote CurrentWiimote
	{
		get
		{
			return detectedWiiMote;
		}
		set
		{
			detectedWiiMote = value;
		}
	}

	public bool DisableMenus
	{
		get
		{
			return disableMenus;
		}
		set
		{
			disableMenus = value;
		}
	}

	public bool ActivateSelectionMenu
	{
		get
		{
			return activateSelectionMenu;
		}
		set
		{
			activateSelectionMenu = value;
		}
	}

	public Furniture[] Furnitures {
		get {
			return this.furnitures;
		}
		set {
			this.furnitures = value;
		}
	}
}
