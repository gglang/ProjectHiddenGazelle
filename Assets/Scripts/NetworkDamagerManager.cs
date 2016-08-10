using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkDamagerManager : NetworkBehaviour
{
    [Command]
    public void CmdDamageTarget(GameObject target, float amount)
    {
        IDamagable damagable = target.GetComponent<IDamagable>();
        damagable.Damage(amount);
    }
}

