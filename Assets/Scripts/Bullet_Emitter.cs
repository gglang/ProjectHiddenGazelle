using UnityEngine;
using System.Collections;

public class Bullet_Emitter : MonoBehaviour {

    public float timeBetweenShots;
    public float bulletsPerShot;
   // public float bulletSpread;
   // public float recoil;
    public float bulletForce;
    public GameObject bullet;

    AudioSource audSource;
    float currentShot;
    bool canFire;
    BulletManager bm;

	// Use this for initialization
	void Start () {
        audSource = gameObject.GetComponent<AudioSource>();
        bm = gameObject.GetComponent<BulletManager>();
        currentShot = 0.0f;
        canFire = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if(canFire)
            {
                fireBullet();
                StartCoroutine(cooldown());
            }
        
        }
	}

    void fireBullet()
    {
        for(int i = 0; i < bulletsPerShot; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(bullet);
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * bulletForce);
            audSource.PlayOneShot(audSource.clip, 1);
            bm.bulletFired(go);
        }

    }

    IEnumerator cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canFire = true;
    }
}
