using UnityEngine;
using System.Collections;

public class WardController : MonoBehaviour {
	public GameObject WardJizz;
	public float JizzDuration = 30f;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Hunter") {
			GameObject jizz = Instantiate(WardJizz, this.transform.position, Quaternion.identity) as GameObject;
			Destroy(jizz, JizzDuration);
			Destroy(this.gameObject);
		}
	}
}
