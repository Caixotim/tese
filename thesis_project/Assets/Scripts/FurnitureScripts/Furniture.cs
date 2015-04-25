public class Furniture {	public enum FurnitureType{floor, ceiling, side};	private int id;	private string name;	private FurnitureType furnitureType;	private string description;	private string price;	private float[] position = new float[3];	private float[] rotation = new float[3];	private float mass;	private string imageUrl;	public Furniture () {	}	public Furniture (int _id_, string _imageUrl_, string _name_, string _furnitureType_, string _description_, string _price_, float _weight_) {		this.id = _id_;		this.imageUrl = _imageUrl_;		this.name = _name_;		this.furnitureType = (FurnitureType) System.Enum.Parse(typeof(FurnitureType), _furnitureType_);		this.description = _description_;		this.price = _price_;		this.mass = CalculateMass (_weight_);	}	public int Id {		get {			return this.id;		}	}	public string ImageUrl {		get {			return this.imageUrl;		}		set {			this.imageUrl = value;		}	}	public string Name {		get{			return this.name;		}	}	public FurnitureType Type {		get {			return this.furnitureType;		}	}	public string Description {		get {			return this.description;		}	}	public string Price {		get {			return this.price;		}	}	public float[] Position {		get {			return this.position;		}		set {			this.position = value;		}	}	public float[] Rotation {		get {			return this.rotation;		}		set {			this.rotation = value;		}	}	public float Mass {		get {			return mass;		}		set {			this.mass = CalculateMass(value);		}	}	private float CalculateMass(float weight) {		return weight * 9.8f;	}}