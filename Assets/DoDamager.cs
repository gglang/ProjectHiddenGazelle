using UnityEngine;
using System.Collections;

public class DoDamager : MonoBehaviour {

    NetworkDamagerManager ndm;
    public float damage = 10f;
   // bool onCooldown;

	// Use this for initialization
	void Start () {
        ndm = gameObject.GetComponentInParent<NetworkDamagerManager>();
	}

    void OnTriggerEnter(Collider collider)
    {
        IDamagable target = collider.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        if (target != null)
        {
            ndm.CmdDamageTarget(collider.gameObject, damage);
       //     StartCoroutine(doCooldown());
        }

    }

   // IEnumerator doCooldown()
   // {
   //     onCooldown = true;
   // //    yield return new WaitForSeconds(0.5f);
   //     onCooldown = false;
   // }
}
