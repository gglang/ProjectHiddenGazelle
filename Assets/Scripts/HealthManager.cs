using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour, IDamagable {

    public const float maxHealth = 500;
	public bool InstaKill = false;	// Debug tool

    [SyncVar(hook = "OnChangeHealth")]
    public float health = maxHealth;

    public bool vulnerable = true;

	public event IDamagableDelegate OnDeath;

	private UnityEngine.UI.Text healthText;

    public bool IsVulnerable(){
        return vulnerable;
    }

	void Start() {
		healthText = GameObject.Find("HealthText").GetComponent<UnityEngine.UI.Text>();
		if(this.isLocalPlayer) {
			healthText.text = "Health: "+health;
		}
	}

	// Update is called once per frame
	void Update () {
		if(this.isLocalPlayer) {
			healthText.text = "Health: "+health;
		}

	    if (health <= 0)
        {
            Die();
        }

		if(InstaKill) {
			Die();
		}
	}

    public bool Damage(float amount)
    {
		
		if (vulnerable && amount > 0)
        {
            health -= amount;
            return true;
		} else if(amount < 0) 
		{
			if((health - amount) > maxHealth) {
				health = maxHealth;
			} else {
				health -= amount;
			}
			return true;
		}
        else
        {
            return false;
        }
    }

    public float HealthFraction()
    {
		return health / maxHealth;
    }

    private void Die()
    {
		Debug.Log("Something died:"+this.gameObject.name);
		if(OnDeath != null) {
			OnDeath();
		}

        Destroy(this.gameObject);
    }

    void OnChangeHealth(float health)
    {

    }
}
