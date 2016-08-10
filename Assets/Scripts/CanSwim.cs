using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ConstantForce))]
public class CanSwim : MonoBehaviour {
	public float buoyancy = 10;
	private GameObject waterPlane;
	private Rigidbody myRb;
	private ConstantForce myCf;
	private bool swimming = false;

	private float sinkDistance = 2f;

	void Start () {
		waterPlane = GameObject.FindGameObjectWithTag("Water");
		myRb = GetComponent<Rigidbody>();
		myCf = GetComponent<ConstantForce>();
	}
	
	void FixedUpdate () {
		if(this.transform.position.y < waterPlane.transform.position.y - sinkDistance /*&& !swimming*/) {
			this.transform.position = new Vector3(this.transform.position.x, waterPlane.transform.position.y - sinkDistance, this.transform.position.z);
//			// swim
//			swimming = true;
//			myRb.useGravity = false;
//			myCf.relativeForce = new Vector3(0,buoyancy,0);
//		} else if(this.transform.position.y >= waterPlane.transform.position.y && swimming){
//			// stop swimming
//			swimming = false;
//			myRb.useGravity = true;
//			myCf.relativeForce = Vector3.zero;
		}
	}
}
