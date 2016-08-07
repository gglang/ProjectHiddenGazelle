using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour, IDamagable {

    [SyncVar]
    public float health = 500;
    bool vulnerable = true;

	private float startingHealth;

	public event IDamagableDelegate OnDeath;

	void Start() {
		startingHealth = health;
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
		if (vulnerable && amount > 0)
        {
            health -= amount;
            return true;
		} else if(amount < 0) 
		{
			if((health - amount) > startingHealth) {
				health = startingHealth;
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

    public float HealthRemaining()
    {
        return health;
    }

    private void Die()
    {
        Debug.LogError("Monster killed!");
		if(OnDeath != null) {
			OnDeath();
		}

        Destroy(this);
    }
}
