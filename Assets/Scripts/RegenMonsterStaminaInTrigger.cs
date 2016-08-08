using UnityEngine;
using System.Collections;

public class RegenMonsterStaminaInTrigger : MonoBehaviour {
	public float RegenAmountPerSecond = 5f;

	private StaminaPool monsterPool;
	private bool regen = false;

	void Start () {
		monsterPool = GameObject.FindGameObjectWithTag("Monster").GetComponent<StaminaPool>();
	}
	
	void Update () {
		if(regen) {
			monsterPool.ChangeStamina(RegenAmountPerSecond * Time.deltaTime);
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
