using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MonsterHealthManager : NetworkBehaviour, IDamagable {

    [SyncVar]
    float health = 500;
    bool vulnerable = true;

	// Use this for initialization
	void Start () {
	
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
        if (vulnerable)
        {
            health -= amount;
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
        Destroy(this);
    }
}
