using UnityEngine;
using System.Collections;

public class ArmAttack : MonoBehaviour {

    public GameObject Arm1;
    public GameObject Arm2;
    public GameObject Arm3;
    public GameObject Arm4;
    public GameObject Arm5;
    public GameObject Arm6;

    public float swingSpeed;
    public float explosionForce;
    public float explosionRadius;
    public float delayBetweenArms;

    Vector3 a1StartingPos;
    Vector3 a2StartingPos;
    Vector3 a3StartingPos;
    Vector3 a4StartingPos;
    Vector3 a5StartingPos;
    Vector3 a6StartingPos;

    bool isAttacking;
	// Use this for initialization
	void Start () {
        isAttacking = false;
        a1StartingPos = Arm1.transform.localPosition;
        a2StartingPos = Arm2.transform.localPosition;
        a3StartingPos = Arm3.transform.localPosition;
        a4StartingPos = Arm4.transform.localPosition;
        a5StartingPos = Arm5.transform.localPosition;
        a6StartingPos = Arm6.transform.localPosition;

    }
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if(isAttacking)
            {
                return;
            }
            activateArms();
            //StartCoroutine(attack());

        }else if(Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            deActivateArms();
            //StopCoroutine(attack());
        }

	}


    void activateArms()
    {
        Arm1.SetActive(true);
        Arm2.SetActive(true);
        Arm3.SetActive(true);
        Arm4.SetActive(true);
        Arm5.SetActive(true);
        Arm6.SetActive(true);
        isAttacking = true;
    }

    void deActivateArms()
    {
        Arm1.SetActive(false);
        Arm2.SetActive(false);
        Arm3.SetActive(false);
        Arm4.SetActive(false);
        Arm5.SetActive(false);
        Arm6.SetActive(false);
        Arm1.transform.localPosition = a1StartingPos;
        Arm2.transform.localPosition = a2StartingPos;
        Arm3.transform.localPosition = a3StartingPos;
        Arm4.transform.localPosition = a4StartingPos;
        Arm5.transform.localPosition = a5StartingPos;
        Arm6.transform.localPosition = a6StartingPos;

        isAttacking = false;
    }
}
