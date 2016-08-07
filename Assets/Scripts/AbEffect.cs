using UnityEngine;
using System.Collections;
using System;

public abstract class AbEffect : MonoBehaviour {
	void Start () {
		// This effect assumes it was instantiated as a child of whatever it is effecting.
		GameObject parent = this.transform.parent.gameObject;
		AbEffect[] siblings = parent.GetComponentsInChildren<AbEffect>();
		foreach(AbEffect sibling in siblings) {
			if(sibling.Equals(this)) {
				continue; // please
			}

			Destroy(this.gameObject);
		}

		this.ActivateEffect();
	}

	abstract protected void ActivateEffect();
	abstract protected void DisactivateEffect();

	void OnDestroy() {
		DisactivateEffect();
	}
}
