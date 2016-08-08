using UnityEngine;
using System.Collections;

public class RegenMonsterHealthInTrigger : MonoBehaviour {
	public float RegenAmountPerSecond = 5f;

	private HealthManager monsterHealth;
	private bool regen = false;

	void Start () {
		monsterHealth = GameObject.FindGameObjectWithTag("Monster").GetComponent<HealthManager>();
	}

	void Update () {
		if(regen) {
			monsterHealth.Damage(-RegenAmountPerSecond * Time.deltaTime);
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
