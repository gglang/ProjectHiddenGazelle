using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour {

    public GameObject attackVolume;

    Vector3 resetPosition;
    AmmoManager am;

	// Use this for initialization
	void Start () {
        am = gameObject.GetComponentInParent<AmmoManager>();
        resetPosition = attackVolume.transform.localPosition;
    }

    void Update()
    {
        Debug.Log(am.getAmmo());
        if(Input.GetMouseButton(1) || Input.GetMouseButtonDown(1))
        {
            if(am.hasAmmo() )
            {
                enableAttack();
            }else
            {
                disableAttack();
            }

        }else if(Input.GetMouseButtonUp(1) || !am.hasAmmo())
        {
            disableAttack();
        }
    }

    void disableAttack()
    {
        attackVolume.SetActive(false);
        attackVolume.transform.localPosition = resetPosition;
    }

    void enableAttack()
    {
        am.useAmmo();
        attackVolume.SetActive(true);
    }
}
