using UnityEngine;
using System.Collections;

public class DoDamager : MonoBehaviour {

    public string[] tagsToDamage;
    NetworkDamagerManager ndm;
    public float damage = 10f;
   // bool onCooldown;

	// Use this for initialization
	void Start () {
        ndm = gameObject.GetComponentInParent<NetworkDamagerManager>();
	}

    void OnTriggerEnter(Collider collider)
    {
        if (!ndm.isLocalPlayer)
            return;

        IDamagable target = collider.gameObject.GetComponent(typeof(IDamagable)) as IDamagable;
        if (target != null && checkTag(collider.gameObject.tag))
        {
            Debug.Log("****HIT****" + collider.gameObject.name);
            ndm.CmdDamageTarget(collider.gameObject, damage);
        }

    }

    bool checkTag(string tag)
    {
        foreach(string stringTags in tagsToDamage)
        {
            if(stringTags.Equals(tag))
            {
                return true;
            }
        }

        return false;
    }

   // IEnumerator doCooldown()
   // {
   //     onCooldown = true;
   // //    yield return new WaitForSeconds(0.5f);
   //     onCooldown = false;
   // }
}
