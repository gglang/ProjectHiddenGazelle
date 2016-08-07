using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour, IDamagable {

    public const float maxHealth = 500;

    [SyncVar(hook = "OnChangeHealth")]
    public float health = maxHealth;

    public bool vulnerable = true;

	private float startingHealth;

	public event IDamagableDelegate OnDeath;

    public bool IsVulnerable(){
        return vulnerable;
    }

	void Start() {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager)
        {
            WinCondition winCon = gameManager.GetComponent<WinCondition>();
            if (IsHunter)
            {
                winCon.AddPlayer(this);
            }
            else
            {
                winCon.AddMonster(this);
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
    		if(!isServer) {
    		return vulnerable;
    		}
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

        Destroy(this.gameObject);
    }

    void OnChangeHealth(float health)
    {

    }
}
