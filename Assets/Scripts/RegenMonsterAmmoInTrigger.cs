using UnityEngine;
using System.Collections;

public class RegenMonsterAmmoInTrigger : MonoBehaviour {
	public float RegenAmountPerSecond = 5f;

	private AmmoManager monsterAmmo;
	private bool regen = false;

	void Start () {
		monsterAmmo = GameObject.FindGameObjectWithTag("Monster").GetComponent<AmmoManager>();
	}
	
	void Update () {
		if(regen) {
			monsterAmmo.putAmmo(RegenAmountPerSecond * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Monster") {
			regen = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Monster") {
			regen = false;
		}
	}
}
