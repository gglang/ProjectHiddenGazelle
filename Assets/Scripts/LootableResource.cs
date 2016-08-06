using UnityEngine;
using System.Collections;

public class LootableResource : MonoBehaviour {
	public int baseValue = 10;
	public bool randomSize = true;

	private int value;
	void Start() {
		if(randomSize) {
			float roll = Utilities.TrueRandomRange(0.1f, 2f);
			this.transform.localScale *= roll;
			value = (int) (baseValue * this.transform.localScale.x);
		} else {
			value = baseValue;
		}
	}

	/// <summary>
	/// Pickup this resource.
	/// @return Value of the resource.
	/// </summary>
	public int pickup() {
		Destroy(this.gameObject);
		return value;
	}
}
