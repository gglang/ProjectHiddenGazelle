using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(TrackInsideTaggedTriggers))]
public class ResourcePickerUpper : NetworkBehaviour {
	public GameObject BuyNodeGUI;

	public GameObject CreepTumour;
	public int CreepTumourCost = 25;

	public GameObject SlowingGoop;
	public int SlowingGoopCost = 25;

	private Text ResourceCountText;
	public int StartingResource = 1000;

	private int CurrentResource;
	private bool alreadyBoughtThisFrame = false;

	private IList<NodeController> nearbyPurchasableNodes;

	private TrackInsideTaggedTriggers creepTracker;

    public bool isHunter;

	void Start() {
		creepTracker = GetComponent<TrackInsideTaggedTriggers>();
		nearbyPurchasableNodes = new List<NodeController>();
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

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Resource")) {
			CurrentResource += other.gameObject.GetComponent<LootableResource>().pickup();
			if(ResourceCountText != null) {
				ResourceCountText.text = "Resources: "+CurrentResource;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Trigger tag: "+other.tag);
		if(other.tag == "Node") {
			NodeController node = other.gameObject.GetComponent<NodeController>();
			if(node.Purchasable()) {
				nearbyPurchasableNodes.Add(node);
				UpdateMyGUI((IList)nearbyPurchasableNodes, BuyNodeGUI);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == "Node") {
			NodeController node = other.GetComponent<NodeController>();
			nearbyPurchasableNodes.Remove(node);
			UpdateMyGUI((IList)nearbyPurchasableNodes, BuyNodeGUI);
		}
	}

	void Update() {
        if (isHunter)
        {
            return;
        }
		if(Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(GamepadInput.XButtonKeyCode())) {
			Debug.Log("BUY NODE INPUT.");
			for(int i = nearbyPurchasableNodes.Count - 1; i >= 0; i--) {
				if(nearbyPurchasableNodes[i] == null) {
					nearbyPurchasableNodes.RemoveAt(i);
				} else if(nearbyPurchasableNodes[i].Purchasable() && nearbyPurchasableNodes[i].PurchaseCost() <= this.CurrentResource) {
					this.CurrentResource -= nearbyPurchasableNodes[i].NodeBaseCost;
					nearbyPurchasableNodes[i].CmdPurchase();
					nearbyPurchasableNodes.RemoveAt(i);
					if(ResourceCountText != null) {
						ResourceCountText.text = "Resources: "+CurrentResource;
					}
					break;
				} else {
					nearbyPurchasableNodes.RemoveAt(i);
				}
			}
			UpdateMyGUI((IList)nearbyPurchasableNodes, BuyNodeGUI);
		}

		if(Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(GamepadInput.YButtonKeyCode())) {
			Debug.Log("BUY CREEP TUMOUR INPUT.");
			if(creepTracker.InsideTaggedTriggers() && CurrentResource >= CreepTumourCost) {
				// Can only build inside creep!
				GameObject tumour = GameObject.Instantiate(CreepTumour, this.transform.position, Quaternion.identity) as GameObject;
                NetworkServer.Spawn(tumour);
				CurrentResource -= CreepTumourCost;
				if(ResourceCountText != null) {
					ResourceCountText.text = "Resources: "+CurrentResource;
				}
			}
		}

		if(Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(GamepadInput.BButtonKeyCode())) {
			Debug.Log("BUY SLOWING GOOP INPUT.");
			if(creepTracker.InsideTaggedTriggers() && CurrentResource >= SlowingGoopCost) {
				// Can only build inside creep!
				GameObject goop = GameObject.Instantiate(SlowingGoop, this.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity) as GameObject;
                NetworkServer.Spawn(goop);
				CurrentResource -= SlowingGoopCost;
				if(ResourceCountText != null) {
					ResourceCountText.text = "Resources: "+CurrentResource;
				}
			}
		}
	}

	private void UpdateMyGUI(IList ifEmptyHideGUI, GameObject GUIObject) {
		for(int i = ifEmptyHideGUI.Count - 1; i >= 0; i--) {
			if(ifEmptyHideGUI[i] == null) {
				ifEmptyHideGUI.RemoveAt(i);
			}
		}

		if(GUIObject == null) {
			return;
		}

		if(ifEmptyHideGUI.Count == 0) {
			GUIObject.SetActive(false);
		} else if(!GUIObject.activeInHierarchy) {
			GUIObject.SetActive(true);
		}
	}
}
