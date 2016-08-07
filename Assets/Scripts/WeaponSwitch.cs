using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour {

    public GameObject rifle;
    public GameObject rocketLauncher;
    public GameObject shotgun;

    int currentWeapon;

	// Use this for initialization
	void Start () {
        rifle.SetActive(true);
    }

    void OnEnable()
    {
        rifle.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            rocketLauncher.SetActive(false);
            shotgun.SetActive(false);
            rifle.SetActive(true);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            rocketLauncher.SetActive(false);
            shotgun.SetActive(true);
            rifle.SetActive(false);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rocketLauncher.SetActive(true);
            shotgun.SetActive(false);
            rifle.SetActive(false);
        }
    }
}
