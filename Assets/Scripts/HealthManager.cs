using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour, IDamagable {

    public const float maxHealth = 500;

    [SyncVar(hook = "OnChangeHealth")]
    public float health = maxHealth;

    public bool vulnerable = true;

	public event IDamagableDelegate OnDeath;

    public bool IsVulnerable(){
        return vulnerable;
    }

	// Update is called once per frame
	void Update () {
	    if (health <= 0)
        {
            Die();
        }
	}

    public bool Damage(float amount)
    {
		if(!isServer) {
			return vulnerable;
		}
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
		Debug.LogError("Something died:"+this.gameObject.name);
		if(OnDeath != null) {
			OnDeath();
		}

        Destroy(this.gameObject);
    }

    void OnChangeHealth(float health)
    {

    }
}
