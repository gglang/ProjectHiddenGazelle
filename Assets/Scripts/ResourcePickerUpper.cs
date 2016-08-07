﻿using UnityEngine;
using System.Collections;

public class ResourcePickerUpper : MonoBehaviour {
	public int StartingResource = 0;

	private int CurrentResource;

	void Start() {
		CurrentResource = StartingResource;
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Resource")) {
			CurrentResource += other.gameObject.GetComponent<LootableResource>().pickup();
		}
	}
}
