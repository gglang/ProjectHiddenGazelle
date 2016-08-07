using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedLineRenderer : NetworkBehaviour
 {
    private LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    [ClientRpc]
    public void RpcSetLineRendererPos(Vector3 start, Vector3 end)
    {
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
