using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(MonsterResourceManager))]
public class MonsterPurchaseManager : NetworkBehaviour {
	public GameObject Barrier;
	public int BarrierCost = 25;

	public GameObject Ward;
	public int WardCost = 25;

	public GameObject CreepTumour;
	public int CreepTumourCost = 25;

	public GameObject SlowingGoop;
	public int SlowingGoopCost = 25;

	private MonsterResourceManager resources;
	private bool alreadyBoughtThisFrame = false;

	private IList<NodeController> nearbyPurchasableNodes;

	private TrackInsideTaggedTriggers creepTracker;
	private ThirdPersonCharacter charController;

	void Start () {
		resources = GetComponent<MonsterResourceManager>();
		charController = GetComponent<ThirdPersonCharacter>();
		creepTracker = GetComponent<TrackInsideTaggedTriggers>();
		nearbyPurchasableNodes = new List<NodeController>();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Trigger tag: "+other.tag);
		if(other.tag == "Node") {
			NodeController node = other.gameObject.GetComponent<NodeController>();
			if(node.Purchasable()) {
				nearbyPurchasableNodes.Add(node);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == "Node") {
			NodeController node = other.GetComponent<NodeController>();
			nearbyPurchasableNodes.Remove(node);
		}
	}

	void Update() {
		HandleInput();
	}

	private void HandleInput() {
		if(!this.isLocalPlayer) {
			return;
		}

		if(!charController.IsGrounded()) {
			return;
		}

		if(Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(GamepadInput.XButtonKeyCode())) {
			Debug.Log("BUY NODE INPUT.");
			for(int i = nearbyPurchasableNodes.Count - 1; i >= 0; i--) {
				if(nearbyPurchasableNodes[i] == null) {
					nearbyPurchasableNodes.RemoveAt(i);
				} else if(nearbyPurchasableNodes[i].Purchasable() && nearbyPurchasableNodes[i].PurchaseCost() <= resources.GetCurrentResource()) {
					resources.SpendResource(nearbyPurchasableNodes[i].NodeBaseCost);
					nearbyPurchasableNodes[i].CmdPurchase();
					nearbyPurchasableNodes.RemoveAt(i);
					break;
				} else {
					nearbyPurchasableNodes.RemoveAt(i);
				}
			}
		}

		if(Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(GamepadInput.YButtonKeyCode())) {
			Debug.Log("BUY CREEP TUMOUR INPUT.");
			if(creepTracker.InsideTaggedTriggers() && resources.GetCurrentResource() >= CreepTumourCost) {
				// Can only build inside creep!
				GameObject tumour = GameObject.Instantiate(CreepTumour, this.transform.position, Quaternion.identity) as GameObject;
				NetworkServer.Spawn(tumour);
				resources.SpendResource(CreepTumourCost);
			}
		}

		if(Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(GamepadInput.BButtonKeyCode())) {
			Debug.Log("BUY SLOWING GOOP INPUT.");
			if(creepTracker.InsideTaggedTriggers() && resources.GetCurrentResource() >= SlowingGoopCost) {
				// Can only build inside creep!
				GameObject goop = GameObject.Instantiate(SlowingGoop, this.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity) as GameObject;
				NetworkServer.Spawn(goop);
				resources.SpendResource(SlowingGoopCost);
			}
		}

		if(Input.GetKeyUp(KeyCode.Alpha4)) {
			Debug.Log("BUY BARRIER INPUT");
			if(creepTracker.InsideTaggedTriggers() && resources.GetCurrentResource() >= BarrierCost) {
				// Can only build inside creep!
				GameObject barrier = GameObject.Instantiate(Barrier, this.transform.position + new Vector3(0f, 0.1f, 0f) + (this.transform.forward.normalized * 2f), Quaternion.identity) as GameObject;
				NetworkServer.Spawn(barrier);
				resources.SpendResource(BarrierCost);
			}
		}

		if(Input.GetKeyUp(KeyCode.Alpha5)) {
			Debug.Log("BUY WARD INPUT");

			// Can build anywhere!
			GameObject ward = GameObject.Instantiate(Ward, this.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity) as GameObject;
			resources.SpendResource(WardCost);
		}
	}
}
