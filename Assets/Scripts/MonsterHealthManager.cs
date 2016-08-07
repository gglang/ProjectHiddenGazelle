using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MonsterHealthManager : NetworkBehaviour, IDamagable {

    public const float maxHealth = 500;

    [SyncVar(hook = "OnChangeHealth")]
    public float health = maxHealth;

	public bool IsHunter = false;
    public bool vulnerable = true;

	public delegate void HealthDelegate();
	public event HealthDelegate OnDeath;

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
        if (!isServer)
        {
            return vulnerable;
        }

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

        Destroy(this.gameObject);
    }

    void OnChangeHealth(float health)
    {

    }
}
