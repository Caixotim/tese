using UnityEngine;
using System.Collections;

public class Furniture : MonoBehaviour{

	public enum FurnitureType {floor, aboveObj, ceil, side};
	private FurnitureType furnitureType;

	// public Furniture(FurnitureType _furnitureType_) {
	// 	this.furnitureType = _furnitureType_;
	// }

	public Furniture() {
		this.furnitureType = FurnitureType.floor;
	}

	public FurnitureType _FurnitureType {
		get {
			return furnitureType;
		}
	}

}
