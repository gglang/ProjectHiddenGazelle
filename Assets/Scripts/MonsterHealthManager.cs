using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MonsterHealthManager : NetworkBehaviour, IDamagable {

    [SyncVar]
    public float health = 500;
	public bool IsHunter = false;
    bool vulnerable = true;

	public delegate void HealthDelegate();
	public event HealthDelegate OnDeath;

	void Start() {
		if(IsHunter) {
			GameObject.Find("GameManager").GetComponent<WinCondition>().AddPlayer(this);
		} else {
			GameObject.Find("GameManager").GetComponent<WinCondition>().AddMonster(this);
		}
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
		if(OnDeath != null) {
			OnDeath();
		}

        Destroy(this);
    }
}
