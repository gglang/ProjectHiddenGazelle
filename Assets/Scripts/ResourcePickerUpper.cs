using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourcePickerUpper : MonoBehaviour {
	public GameObject BuyNodeGUI;
	private Text ResourceCountText;
	public int StartingResource = 0;

	private int CurrentResource;
	private bool alreadyBoughtThisFrame = false;

	private bool BuyNodeInput = false;

	void Start() {
		ResourceCountText = GameObject.Find("ResourcesText").GetComponent<Text>();	// HACK late nights eh bud?
		CurrentResource = StartingResource;
		ResourceCountText.text = "Resources: "+StartingResource;
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Resource")) {
			CurrentResource += other.gameObject.GetComponent<LootableResource>().pickup();
			ResourceCountText.text = "Resources: "+CurrentResource;
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Trigger tag: "+other.tag);
		if(other.tag == "Node") {
			NodeController node = other.gameObject.GetComponent<NodeController>();
			if(node.Purchasable()) {
				StartCoroutine(TurnOnBuyNodeGUI());
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if(BuyNodeInput) {
			// Buy nearby node
			if(other.tag == "Node") {
				NodeController node = other.GetComponent<NodeController>();
				if(node.Purchasable() && this.CurrentResource >= node.PurchaseCost()) {
					node.Purchase();
					this.CurrentResource -= node.PurchaseCost();
				}
				BuyNodeInput = false;
			}
		}

	}

	void Update() {
		if(Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(GamepadInput.YButtonKeyCode())) {
			Debug.Log("BUY NODE INPUT.");
			StartCoroutine(TriggerBuyNodeInput());
		}
	}

	private IEnumerator TriggerBuyNodeInput() {
		BuyNodeInput = true;
		yield return new WaitForSeconds(0.5f);
		BuyNodeInput = false;
	}

	private IEnumerator TurnOnBuyNodeGUI() {
		if(BuyNodeGUI.activeInHierarchy == true) {
			yield break;
		}

		BuyNodeGUI.SetActive(true);
		yield return new WaitForSeconds(0.95f);
		BuyNodeGUI.SetActive(false);
	}


}
