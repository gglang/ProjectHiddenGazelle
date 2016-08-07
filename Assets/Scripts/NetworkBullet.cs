using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkBullet : NetworkBehaviour {

    [SyncVar]
    public Vector3 start;
    [SyncVar]
    public Vector3 end;

    private LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
	}
}
