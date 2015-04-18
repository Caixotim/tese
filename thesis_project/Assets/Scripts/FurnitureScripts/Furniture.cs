
public class Furniture {

	public enum FurnitureType{floor, ceiling, side};

	private int id;
	private string name;
	private FurnitureType furnitureType;
	private string characteristics;
	private string price;
	private float[] position = new float[3];
	private float[] rotation = new float[3];

	public Furniture () {
	}

	public Furniture (int _id_, string _name_, string _furnitureType_, string _characteristics_, string _price_) {
		this.id = _id_;
		this.name = _name_;
		this.furnitureType = (FurnitureType) System.Enum.Parse(typeof(FurnitureType), _furnitureType_);
		this.characteristics = _characteristics_;
		this.price = _price_;
	}

	public int Id {
		get {
			return this.id;
		}
	}

	public string Name {
		get{
			return this.name;
		}
	}

	public FurnitureType Type {
		get {
			return this.furnitureType;
		}
	}

	public string Characteristics {
		get {
			return this.characteristics;
		}
	}

	public string Price {
		get {
			return this.price;
		}
	}

	public float[] Position {
		get {
			return this.position;
		}
		set {
			this.position = value;
		}
	}

	public float[] Rotation {
		get {
			return this.rotation;
		}
		set {
			this.rotation = value;
		}
	}
}
