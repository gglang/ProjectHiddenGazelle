using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[RequireComponent(typeof(TrackInsideTaggedTriggers))]
[RequireComponent(typeof(HealthManager))]
public class NodeController : NetworkBehaviour, IPurchasable {
	public event IDamagableDelegate BeforeDeath;

	public int NodeBaseCost = 50;
	public float StartingBaseHealth = 100f;
	public LootTableEntry[] Drops;

	public float CreepSpreadTime = 60;
	public float CreepScaleFactor = 2f;
	public GameObject CreepObject;

	public Texture MonsterTex; 
	public Texture NeutralTex;

	private readonly float DROP_OFFSET = 5f;

	private float scaleFactor;

	private float Health;

	private int Quantity = 1;
	private int cost;

	private bool dying = false;

	private TrackInsideTaggedTriggers creepTracker;

	[SerializeField]
	public struct LootTableEntry {
		public GameObject drop;
		public int count;
	}

	public enum ControlState { Monster, Neutral };

	private ControlState controlState;
	private Material damageTexture;

	void Start() {
		creepTracker = GetComponent<TrackInsideTaggedTriggers>();
		controlState = ControlState.Neutral;
		float roll = Utilities.TrueRandomRange(0.5f, 2f);
		scaleFactor = roll;
		this.transform.localScale *= roll;
		Health = StartingBaseHealth * roll;
//		cost = (int) (NodeBaseCost * this.transform.localScale.x);
		cost = NodeBaseCost;
		foreach(Material mat in this.GetComponent<Renderer>().materials) {
			if(mat.name == "Cracks (Instance)") {
				damageTexture = mat;
				damageTexture.SetColor("_Color", new Color(damageTexture.color.r, damageTexture.color.g, damageTexture.color.b, 0f));
			}
			if(mat.name == "Magic (Instance)") {
				mat.mainTexture = NeutralTex;
			}
		}
		GetComponent<HealthManager>().OnDeath += Die;
	}

	private void Die() {
		BeforeDeath();
		foreach(LootTableEntry entry in Drops) {
			for(int i = 0; i < entry.count; i++) {
				Vector3 spawnLocation = this.transform.position + new Vector3(Utilities.TrueRandomRange(0f,DROP_OFFSET), 2f, Utilities.TrueRandomRange(0f,DROP_OFFSET));
				GameObject drop = Instantiate(entry.drop, spawnLocation, Quaternion.identity) as GameObject;
                NetworkServer.Spawn(drop);
			}
		}

		Destroy(this.gameObject);
	}

	#region IPurchasable implementation

	public int PurchaseCost () {
		return cost;
	}

	public bool Purchasable () {
		if(controlState == ControlState.Neutral && creepTracker.InsideTaggedTriggers()) {
			return true;
		}
		return false;
	}

    [Command]
	public void CmdPurchase () {
		if(Quantity > 0) {
			Quantity--;
			controlState = ControlState.Monster;

			CreepSpreader creepSpreader = this.gameObject.GetComponent<CreepSpreader>();
            RpcEnableCreep();
			creepSpreader.CreepScaleFactor = this.CreepScaleFactor;
			creepSpreader.CreepSpreadTime = this.CreepSpreadTime;
			//creepSpreader.CreepObject = this.CreepObject;
		}
	}

    [ClientRpc]
    public void RpcEnableCreep()
    {
        foreach (Material mat in this.GetComponent<Renderer>().materials)
        {
            if (mat.name == "Magic (Instance)")
            {
                mat.mainTexture = MonsterTex;
            }
        }

        CreepSpreader creepSpreader = this.gameObject.GetComponent<CreepSpreader>();
        creepSpreader.enabled = true;
        creepSpreader.RpcSpreadCreep();
    }

	#endregion
}
