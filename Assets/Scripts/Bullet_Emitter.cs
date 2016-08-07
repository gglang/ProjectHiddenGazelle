using UnityEngine;
using System.Collections;

public class Bullet_Emitter : MonoBehaviour {

    public float timeBetweenShots;
    public float bulletsPerShot;
    public GameObject hitmarker;
   // public float bulletSpread;
   // public float recoil;
    public float bulletForce;
    public GameObject bullet;
    public GameObject bulletDecal;
    public GameObject muzzleFlash;

    AudioSource audSource;
    float currentShot;
    bool canFire;
    BulletManager bm;

    Vector3 localPosition;
    float damagePerShot = 15f;

	// Use this for initialization
	void Start () {
        localPosition = transform.localPosition;
        audSource = gameObject.GetComponent<AudioSource>();
        bm = gameObject.GetComponent<BulletManager>();
        currentShot = 0.0f;
        canFire = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.DrawRay(transform.position, transform.position+transform.forward*10, Color.blue, 5f, false);
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if(canFire)
            {
                localPosition = transform.localPosition;
                fireBullet();
                StartCoroutine(cooldown());
            }
        
        } 

        
	}

    void fireBullet()
    {
        for(int i = 0; i < bulletsPerShot; i++)
        {
            Debug.LogWarning("shooting");
            Ray ray = new Ray(transform.position, transform.forward*100);
            RaycastHit hit;
            GameObject temp = (GameObject)Instantiate(muzzleFlash,gameObject.transform.position, Quaternion.identity);
            StartCoroutine(deSpawnHitMarker(temp));
            audSource.PlayOneShot(audSource.clip, 1);
            bool rayHit = Physics.Raycast(ray, out hit, 500f);
            Debug.DrawRay(transform.position, transform.forward*100, Color.white, .5f, true);
            if (rayHit)
            {
                temp = (GameObject)Instantiate(muzzleFlash, hit.point, Quaternion.identity);
                StartCoroutine(deSpawnHitMarker(temp));
                IDamagable target = hit.collider.GetComponent(typeof(IDamagable)) as IDamagable;
                if (target != null)
                {
                    bool tookDamage = target.Damage(damagePerShot);
                    if (tookDamage)
                    {
                        // draw hitmarker
                        GameObject hm = GameObject.Instantiate<GameObject>(hitmarker);
                        hitmarker.transform.position = hit.point;
                        StartCoroutine(deSpawnHitMarker(hm));
                        Debug.LogWarning("Hit! dealt " + damagePerShot + "damage.");
                    }
                }
            }
            
            /*GameObject go = GameObject.Instantiate<GameObject>(bullet);
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * bulletForce);
            audSource.PlayOneShot(audSource.clip, 1);
            bm.bulletFired(go);*/
            
        }

    }

    IEnumerator cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canFire = true;
    }

    IEnumerator deSpawnHitMarker(GameObject go)
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(go);
    }
}
