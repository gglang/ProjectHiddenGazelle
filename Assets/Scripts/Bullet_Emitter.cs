using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

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

    private NetworkBulletManager nbm;

    AudioSource audSource;
    float currentShot;
    bool canFire;

    Vector3 localPosition;
    float damagePerShot = 15f;

    private bool m_isLocalPlayer;

	// Use this for initialization
	void Start () {
        localPosition = transform.localPosition;
        audSource = gameObject.GetComponent<AudioSource>();
        //networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        //bm = networkManager.GetComponent<BulletManager>();
        nbm = GetComponentInParent<NetworkBulletManager>();
        currentShot = 0.0f;
        canFire = true;

        FirstPersonController controller = GetComponentInParent<FirstPersonController>();
        m_isLocalPlayer = controller.isLocalPlayer;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_isLocalPlayer)
        {
            return;
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if(canFire)
            {
                //fireBullet();
                Fire();
                localPosition = transform.localPosition;
                StartCoroutine(cooldown());
            }
        
        } 

        
	}

    public void Fire()
    {
        for(int i = 0; i < bulletsPerShot; i++)
        {
            Debug.LogWarning("shooting");
            Ray ray = new Ray(transform.position, transform.forward*100);
            RaycastHit hit;
            GameObject temp = (GameObject)Instantiate(muzzleFlash, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 0.5f), Quaternion.identity);
            StartCoroutine(deSpawnHitMarker(temp));
            audSource.PlayOneShot(audSource.clip, 1);
            bool rayHit = Physics.Raycast(ray, out hit, 500f);
            //Debug.DrawRay(transform.position, transform.forward*100, Color.white, .5f, true);
            if (rayHit)
            {
                temp = (GameObject)Instantiate(muzzleFlash, hit.point, Quaternion.identity);
                StartCoroutine(deSpawnHitMarker(temp));
                IDamagable target = hit.collider.GetComponent(typeof(IDamagable)) as IDamagable;
                if (target != null)
                {
                    //bool tookDamage = target.Damage(damagePerShot);
                    nbm.CmdDamageTarget(hit.collider.gameObject, damagePerShot);
                    if (target.IsVulnerable())
                    {
                        // draw hitmarker
                        GameObject hm = GameObject.Instantiate<GameObject>(hitmarker);
                        hitmarker.transform.position = hit.point;
                        StartCoroutine(deSpawnHitMarker(hm));
                        Debug.LogWarning("Hit! dealt " + damagePerShot + "damage.");
                    }
                }
            }

            nbm.CmdFire(transform.position, rayHit ? hit.point : transform.position + transform.forward * 200f);

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
