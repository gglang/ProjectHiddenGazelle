using UnityEngine;
using System.Collections;

public class DoDamager : MonoBehaviour {

    NetworkDamagerManager ndm;

	// Use this for initialization
	void Start () {
        ndm = gameObject.GetComponentInParent<NetworkDamagerManager>();
	}

    void OnCollisionEnter(Collision collision)
    {
        IDamagable target = collision.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        if (target != null)
        {
            ndm.CmdDamageTarget(collision.gameObject, 30f);
        }

    }
}
