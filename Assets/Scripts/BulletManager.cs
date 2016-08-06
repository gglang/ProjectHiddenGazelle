using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    GameObject[] bullets;
    int currentCount;

	// Use this for initialization
	void Start () {
        currentCount = 0;
	}

    public void bulletFired(GameObject go)
    {
        if(currentCount == 50)
        {
            Destroy(bullets[Random.Range(0, 49)]);
        }
    }
}
