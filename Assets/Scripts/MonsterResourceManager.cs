using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(TrackInsideTaggedTriggers))]
public class MonsterResourceManager : NetworkBehaviour {
	private Text ResourceCountText;
	public int StartingResource = 1000;

	private int CurrentResource;

	void Start() {
		CurrentResource = StartingResource;

		try {
			ResourceCountText = GameObject.Find("ResourcesText").GetComponent<Text>();	// HACK late nights eh bud?
		} catch (NullReferenceException e) {
			Debug.LogWarning("No ResourcesText named gameobject in the scene; need this to display the character's resource count.");
		}

		if(ResourceCountText != null) {
			ResourceCountText.text = "Resources: "+StartingResource;
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Trigger tag: "+other.tag);

		if(other.gameObject.tag == "Resource") {
			CurrentResource += other.gameObject.GetComponent<LootableResource>().pickup();
			if(ResourceCountText != null) {
				ResourceCountText.text = "Resources: "+CurrentResource;
			}
		}
	}

	public int GetCurrentResource() {
		return CurrentResource;
	}

	public void SpendResource(int amount) {
		CurrentResource -= amount;
		if(ResourceCountText != null) {
			ResourceCountText.text = "Resources: "+CurrentResource;
		}
	}
}
