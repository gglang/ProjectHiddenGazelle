using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkBulletManager : NetworkBehaviour {

    public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    [Command]
    public void CmdFire(Vector3 start, Vector3 end)
    {
        GameObject newBullet = Instantiate(bullet) as GameObject;

        NetworkBullet nb = newBullet.GetComponent<NetworkBullet>();
        if (nb)
        {
            nb.start = start;
            nb.end = end;
        }

        Destroy(newBullet, 1.0f);
        NetworkServer.Spawn(newBullet);
    }

    [Command]
    public void CmdDamageTarget(GameObject target, float amount)
    {
        IDamagable damagable = target.GetComponent<IDamagable>();
        damagable.Damage(amount);
    }
}
