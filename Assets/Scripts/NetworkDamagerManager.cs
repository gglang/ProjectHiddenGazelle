using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkDamagerManager : NetworkBehaviour
{
    public GameObject attackVolume;

    [Command]
    public void CmdDamageTarget(GameObject target, float amount)
    {
        IDamagable damagable = target.GetComponent<IDamagable>();
        damagable.Damage(amount);
    }

    [ClientRpc]
    public void RpcEnableAttackVolume(bool enable)
    {
        attackVolume.SetActive(enable);
    }
}

