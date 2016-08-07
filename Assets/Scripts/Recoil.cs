using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour {

    public float xLeanAmount;
    public float yLeanAmount;
    public float zleanAmount;
    public bool isRifle;


    Vector3 resetPosition;
    Vector3 ironSightResetPosition;
    Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        resetPosition = transform.localPosition;
        targetPosition = resetPosition;
        ironSightResetPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);

	}
	
	// Update is called once per frame
	void Update () {
	
        //shooting
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            //shooting but not aiming
            if(!Input.GetMouseButton(1))
            {
                targetPosition = resetPosition;
            }else
            {
                targetPosition = ironSightResetPosition;
            }
            StartCoroutine(recoil());
        }else if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1)) //not shooting and not aiming
        {
            transform.localPosition = resetPosition;
        }else  if(Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            transform.localPosition = ironSightResetPosition;
        }else
        {
            transform.localPosition = resetPosition;
        }
	}
    
    IEnumerator recoil()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x + Utilities.TrueRandomRange(-xLeanAmount, xLeanAmount), transform.localPosition.y + Utilities.TrueRandomRange(-yLeanAmount, yLeanAmount), transform.localPosition.z + Utilities.TrueRandomRange(-zleanAmount, zleanAmount)), 0.5f);
        yield return new WaitForSeconds(0.07f);
        if (isRifle)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.4f);
        }else
        {
            transform.localPosition = targetPosition;
        }
    }

}
