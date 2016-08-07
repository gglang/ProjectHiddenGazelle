using UnityEngine;
using System.Collections;

public class HiveController : MonoBehaviour, IDamagable {
	public float StartingHealth = 500f;
	public float CreepSpreadTime = 15f;
	public GameObject CreepObject;

	public delegate void HiveDelegate();
	public event HiveDelegate OnDeath;

	private GameObject creep;
	private float Health;

	private Material damageTexture;

	void Start() {
		Health = StartingHealth;
		foreach(Material mat in this.GetComponent<Renderer>().materials) {
			if(mat.name == "Cracks (Instance)") {
				damageTexture = mat;
				damageTexture.SetColor("_Color", new Color(damageTexture.color.r, damageTexture.color.g, damageTexture.color.b, 0f));
			}
		}

		GameObject.Find("GameManager").GetComponent<WinCondition>().AddHive(this);

		StartCoroutine(SpreadCreep());
	}

	private IEnumerator SpreadCreep() {
		creep = Instantiate(CreepObject, this.transform.position, Quaternion.identity) as GameObject;
		creep.transform.SetParent(this.transform);
		Vector3 startScale = creep.transform.localScale * 2f;
		creep.transform.localScale = Vector3.zero;

		float creepTime = Time.time + CreepSpreadTime;
		while(creepTime > Time.time) {
			float progress = Mathf.Lerp(0f, 2f, (creepTime - Time.time) / CreepSpreadTime);
			progress = 2f - progress;
			creep.transform.localScale = Vector3.Lerp(Vector3.zero, startScale, progress);
			yield return new WaitForFixedUpdate();
		}

		yield break;
	}

	#region IDamagable implementation

	public float HealthRemaining() {
		return Health;
	}

	public bool Damage (float amount) {
		Health -= amount;

		if(damageTexture != null) {
			damageTexture.SetColor("_Color", new Color(damageTexture.color.r, damageTexture.color.g, damageTexture.color.b, (StartingHealth - Health) / StartingHealth));
		}

		if(Health <= 0) {
			if(OnDeath != null) {
				OnDeath();
				Destroy(this.gameObject);
			}
		}		

		return true;
	}

    public bool IsVulnerable()
    {
        return true;
    }

	#endregion
}
