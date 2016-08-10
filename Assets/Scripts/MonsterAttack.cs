using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour {

    public GameObject attackVolume;
    public float rotationSpeed;
    public float cooldownSec = .5f;
    public float durationSec = 0.25f;
    private float cooldownProgress = 0f;

    Vector3 resetPosition;
    AmmoManager am;

    NetworkDamagerManager ndm;

	// Use this for initialization
	void Start () {
        am = gameObject.GetComponentInParent<AmmoManager>();
        ndm = gameObject.GetComponentInParent<NetworkDamagerManager>();
        resetPosition = attackVolume.transform.localPosition;
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed);
        if (ndm.isLocalPlayer)
        {
            if (cooldownProgress > durationSec)
            {
                disableAttack();
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (am.hasAmmo())
                {
                    if (cooldownProgress > cooldownSec)
                    {
                        enableAttack();
                        cooldownProgress = 0f;
                    }
                }
                else
                {
                    disableAttack();
                }
            }
            else if (Input.GetMouseButtonUp(1) || !am.hasAmmo())
            {
                disableAttack();
            }
        }
        cooldownProgress += Time.deltaTime;
    }

    void disableAttack()
    {
        attackVolume.SetActive(false);
        attackVolume.transform.localPosition = resetPosition;
        ndm.RpcEnableAttackVolume(false);
    }

    void enableAttack()
    {
        am.useAmmo();
        attackVolume.SetActive(true);
        ndm.RpcEnableAttackVolume(true);
    }
}
