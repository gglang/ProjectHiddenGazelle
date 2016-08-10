using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour {

    public GameObject rifle;
    public GameObject rocketLauncher;
    public GameObject shotgun;

    private NetworkBulletManager nbm;

    int currentWeapon;

	// Use this for initialization
	void Start () {
        nbm = GetComponentInParent<NetworkBulletManager>();
        rifle.SetActive(true);
    }

    void OnEnable()
    {
        rifle.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (!nbm.isLocalPlayer)
        {
            return;
        }
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            rocketLauncher.SetActive(false);
            shotgun.SetActive(false);
            rifle.SetActive(true);
			AmmoManager ammoMan = rifle.GetComponent<AmmoManager>();
			if(ammoMan != null) {
				ammoMan.UpdateAmmoUI();
			}
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            rocketLauncher.SetActive(false);
            shotgun.SetActive(true);
            rifle.SetActive(false);
			AmmoManager ammoMan = shotgun.GetComponent<AmmoManager>();
			if(ammoMan != null) {
				ammoMan.UpdateAmmoUI();
			}
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rocketLauncher.SetActive(true);
            shotgun.SetActive(false);
            rifle.SetActive(false);
			AmmoManager ammoMan = rocketLauncher.GetComponent<AmmoManager>();
			if(ammoMan != null) {
				ammoMan.UpdateAmmoUI();
			}
        }
    }
}
