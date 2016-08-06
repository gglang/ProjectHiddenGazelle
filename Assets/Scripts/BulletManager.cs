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
        if(currentCount == 20)
        {
            for(int i = 0; i < 20; i++)
            {
                Destroy(bullets[i]);
            }
        }
    }
}
