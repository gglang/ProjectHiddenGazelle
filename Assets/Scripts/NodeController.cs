using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NodeController : NetworkBehaviour, IPurchasable, IDamagable
{
	public LootTableEntry[] Drops;
	public GameObject CreepObject;

	public Texture MonsterTex; 
	public Texture NeutralTex;

	private readonly float DROP_OFFSET = 5f;


	private GameObject creep;
	private bool dying = false;

	[SerializeField]
	public struct LootTableEntry {
		public GameObject drop;
		public int count;
	}

	public enum ControlState { Monster, Neutral };

    private Material damageTexture;

    [SyncVar]
	private ControlState controlState;
    private float scaleFactor;
    private float Health;
    private int Quantity = 1;
    private int cost;
    public int NodeBaseCost = 50;
    public float StartingBaseHealth = 100f;
    public float CreepSpreadTime = 30f;

    void Start() {
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
	}

	private IEnumerator Die() {
		dying = true;
		foreach(LootTableEntry entry in Drops) {
			for(int i = 0; i < entry.count; i++) {
				Vector3 spawnLocation = this.transform.position + new Vector3(Utilities.TrueRandomRange(0f,DROP_OFFSET), 2f, Utilities.TrueRandomRange(0f,DROP_OFFSET));
				Instantiate(entry.drop, spawnLocation, Quaternion.identity);
			}
		}

		Vector3 startScale = creep.transform.localScale * scaleFactor;
		float dieTime = Time.time + CreepSpreadTime;
		while(dieTime > Time.time) {
			float progress = Mathf.Lerp(0f, 1f, (dieTime - Time.time) / CreepSpreadTime);
			creep.transform.localScale = Vector3.Lerp(Vector3.zero, startScale, progress);
			yield return new WaitForFixedUpdate();
		}

		Destroy(this.gameObject);
		yield break;
	}

    [ClientRpc]
    private void RpcSpreadCreep()
    {
        foreach (Material mat in this.GetComponent<Renderer>().materials)
        {
            if (mat.name == "Magic (Instance)")
            {
                mat.mainTexture = MonsterTex;
            }
        }

        StartCoroutine(SpreadCreep());
    }
    
	private IEnumerator SpreadCreep() {
        creep = Instantiate(CreepObject, this.transform.position, Quaternion.identity) as GameObject;
		creep.transform.SetParent(this.transform);
		Vector3 startScale = creep.transform.localScale;
		creep.transform.localScale = Vector3.zero;

		float creepTime = Time.time + CreepSpreadTime;
		while(creepTime > Time.time) {
			if(dying) {
				break;
			}

			float progress = Mathf.Lerp(0f, scaleFactor, (creepTime - Time.time) / CreepSpreadTime);
			progress = scaleFactor - progress;
			creep.transform.localScale = Vector3.Lerp(Vector3.zero, startScale, progress);
			yield return new WaitForFixedUpdate();
		}

		yield break;
	}

	private bool InCreep() {
		return true;	// TODO implement
	}

	#region IPurchasable implementation

	public int PurchaseCost () {
		return cost;
	}

	public bool Purchasable () {
		if(controlState == ControlState.Neutral && InCreep()) {
			return true;
		}
		return false;
	}

    [Command]
	public void CmdPurchase() {
		if(Quantity > 0) {
			Quantity--;
			controlState = ControlState.Monster;
            RpcSpreadCreep();
		} else {
		}
	}

	#endregion

	#region IDamagable implementation

	public float HealthRemaining() {
		return Health;
	}

	public bool Damage (float amount) {
		if(controlState != ControlState.Monster) {
			return false;
		}

		Health -= amount;

		if(damageTexture != null) {
			damageTexture.SetColor("_Color", new Color(damageTexture.color.r, damageTexture.color.g, damageTexture.color.b, (StartingBaseHealth - Health) / StartingBaseHealth));
		}

		if(Health <= 0) {
			this.Die();
		}		

		return true;
	}

    public bool IsVulnerable()
    {
        return true;
    }

	#endregion
}
