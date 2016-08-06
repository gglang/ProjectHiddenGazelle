using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour {

    GameObject rifle;
    GameObject rocketLauncher;
    GameObject shotgun;

    GameObject[] weapons;

    int currentWeapon;

	// Use this for initialization
	void Start () {
        currentWeapon = 0;
        rifle = GameObject.FindGameObjectWithTag("rifle");
        rocketLauncher = GameObject.FindGameObjectWithTag("rl");
        shotgun = GameObject.FindGameObjectWithTag("shotgun");
        weapons[0] = rifle;
        weapons[1] = rocketLauncher;
        weapons[2] = shotgun;
        rifle.SetActive(true);
        rocketLauncher.SetActive(false);
        shotgun.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weapons[currentWeapon].SetActive(false);
            currentWeapon = ++currentWeapon % 3;
            weapons[currentWeapon].SetActive(true);
        }else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weapons[currentWeapon].SetActive(false);
            currentWeapon = --currentWeapon % 3;
            weapons[currentWeapon].SetActive(true);
        }
    }
}
