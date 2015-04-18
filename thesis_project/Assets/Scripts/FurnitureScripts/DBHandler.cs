using System.Collections;
using System.Collections.Generic;

public class DBHandler{

	private static DBHandler instance = null;

	private DBHandler() {

	}

	public static DBHandler Instance {
		get {
			if(instance == null) {
				instance = new DBHandler();
			}
			return instance;
		}
	}

	public Furniture[] GetFurnitures () {
		Furniture[] furnitures;
		furnitures = new Furniture[1];
		furnitures[0] = new Furniture(1, "Cubo", "floor", "default characteristics", "200€");
		return furnitures;
	}
}
