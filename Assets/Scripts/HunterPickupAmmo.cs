using UnityEngine;
using System.Collections;

public class HunterPickupAmmo : MonoBehaviour {
	[SerializeField]
	AmmoManager ammoManager;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Resource") {
			ammoManager.putAmmo(other.gameObject.GetComponent<LootableResource>().pickup());
		}
	}
}
