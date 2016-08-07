using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyEffectOnTriggerEnter : MonoBehaviour {
	public string ApplyToTag;
	public GameObject effect;

	void OnTriggerEnter(Collider other) {
		if(other.tag.Equals(ApplyToTag)) {
			GameObject effectCreated = Instantiate(effect, other.transform.position, Quaternion.identity) as GameObject;
			effectCreated.transform.SetParent(other.transform);
		}
	}
}
