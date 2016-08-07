using UnityEngine;
using System.Collections;

public class SmashAttack : MonoBehaviour
{

    public GameObject Arm1;
    public GameObject Arm2;

    Transform a1StartingPos;
    Transform a2StartingPos;

    bool isAttacking;
    // Use this for initialization
    void Start()
    {
        isAttacking = false;
        a1StartingPos = Arm1.transform;
        a2StartingPos = Arm2.transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1) || Input.GetMouseButtonDown(1))
        {
            if (isAttacking)
            {
                return;
            }
            activateArms();
            StartCoroutine(attack());

        }
        else if (Input.GetMouseButtonUp(1))
        {
            isAttacking = false;
            deActivateArms();
            StopCoroutine(attack());
        }

    }


    void activateArms()
    {
        Arm1.SetActive(true);
        Arm2.SetActive(true);
        isAttacking = true;
    }

    void deActivateArms()
    {
        Arm1.SetActive(false);
        Arm2.SetActive(false);
        Arm1.transform.localPosition = a1StartingPos.localPosition;
        Arm2.transform.localPosition = a2StartingPos.localPosition;
        Arm1.transform.rotation = a1StartingPos.rotation;
        Arm2.transform.rotation = a2StartingPos.rotation;
        isAttacking = false;
    }

    IEnumerator attack()
    {
            GameObject.Instantiate<GameObject>(Arm1);
            GameObject.Instantiate<GameObject>(Arm2);
            yield return new WaitForSeconds(1.0f);
    }
    
}
