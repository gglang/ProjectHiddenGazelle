using UnityEngine;
using System.Collections;

public class CreepSpreader : MonoBehaviour {
	public float CreepSpreadTime;
	public float CreepScaleFactor = 1f;
	public GameObject CreepObject;
	private GameObject creep;
	private bool dying = false;

	void Start () {
		StartCoroutine(SpreadCreep());
		IDamagable damageController = this.GetComponent<IDamagable>();
		if(damageController != null) {
			damageController.OnDeath += HandleDeath;
		}
	}
	
	private IEnumerator SpreadCreep() {
		creep = Instantiate(CreepObject, this.transform.position, Quaternion.identity) as GameObject;
		creep.transform.SetParent(this.transform);
		Vector3 startScale = creep.transform.localScale * 2f;
		creep.transform.localScale = Vector3.zero;

		float creepTime = Time.time + CreepSpreadTime;
		while(creepTime > Time.time) {
			if(dying) {
				break;
			}

			float progress = Mathf.Lerp(0f, CreepScaleFactor, (creepTime - Time.time) / CreepSpreadTime);
			progress = CreepScaleFactor - progress;
			creep.transform.localScale = Vector3.Lerp(Vector3.zero, startScale, progress);
			yield return new WaitForFixedUpdate();
		}

		yield break;
	}

	private void HandleDeath() {
		this.transform.SetParent(null);
		StartCoroutine(Die());
	}

	private IEnumerator Die() {
		dying = true;
		Vector3 startScale = creep.transform.localScale * CreepScaleFactor;
		float dieTime = Time.time + CreepSpreadTime;
		while(dieTime > Time.time) {
			float progress = Mathf.Lerp(0f, 1f, (dieTime - Time.time) / CreepSpreadTime);
			creep.transform.localScale = Vector3.Lerp(Vector3.zero, startScale, progress);
			yield return new WaitForFixedUpdate();
		}

		Destroy(this.gameObject);
		yield break;
	}
}
