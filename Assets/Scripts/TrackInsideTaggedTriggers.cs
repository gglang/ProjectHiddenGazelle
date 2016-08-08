using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackInsideTaggedTriggers : MonoBehaviour {
	public string TagToTrack;
	private IList<GameObject> thingsImInside;

    void Awake()
    {
        thingsImInside = new List<GameObject>();
    }

	void OnTriggerEnter(Collider other) {
		if(thingsImInside == null) {
			thingsImInside = new List<GameObject>();
		}

		if(other.tag == TagToTrack) {
			thingsImInside.Add(other.gameObject);
			UpdateThingList();
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == TagToTrack) {
			thingsImInside.Remove(other.gameObject);
			UpdateThingList();
		}
	}

	private void UpdateThingList() {
		for(int i = thingsImInside.Count - 1; i >= 0; i--) {
			if(thingsImInside[i] == null) {
				thingsImInside.RemoveAt(i);
			}
		}
	}

	public bool InsideTaggedTriggers() {
		UpdateThingList();
		if(thingsImInside.Count == 0) {
			return false;
		}
		return true;
	}
}
