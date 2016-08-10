using UnityEngine;
using System.Collections;

public class HunterPickupAmmo : MonoBehaviour {
	[SerializeField]
	AmmoManager ammoManager;

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Resource")) {
			ammoManager.putAmmo(other.gameObject.GetComponent<LootableResource>().pickup());
		}
	}
}
