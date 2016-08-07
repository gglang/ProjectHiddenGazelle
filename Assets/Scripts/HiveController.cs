using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthManager))]
public class HiveController : MonoBehaviour {
	public event IDamagableDelegate BeforeDeath;

	private Material damageTexture;

	void Start() {
		foreach(Material mat in this.GetComponent<Renderer>().materials) {
			if(mat.name == "Cracks (Instance)") {
				damageTexture = mat;
				damageTexture.SetColor("_Color", new Color(damageTexture.color.r, damageTexture.color.g, damageTexture.color.b, 0f));
			}
		}

		GameObject.Find("GameManager").GetComponent<WinCondition>().AddHive(this);
		GetComponent<HealthManager>().OnDeath += Die;
	}

	private void Die () {
		if(BeforeDeath != null) {
			BeforeDeath();
		}

		Destroy(this.gameObject);
	}
}
