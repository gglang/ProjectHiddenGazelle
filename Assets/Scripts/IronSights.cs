using UnityEngine;
using System.Collections;

public class IronSights : MonoBehaviour {

    float startingX;
    float startingY;
    float startingZ;

	// Use this for initialization
	void Start () {
        startingX = gameObject.transform.localPosition.x;
        startingY = gameObject.transform.localPosition.y;
        startingZ = gameObject.transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(1))
        {
            gameObject.transform.localPosition = new Vector3(0, startingY, startingZ);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(startingX, startingY, startingZ);
        }

	}
}
