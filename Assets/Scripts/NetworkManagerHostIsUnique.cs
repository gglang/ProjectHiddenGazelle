using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerHostIsUnique : NetworkManager {

    public GameObject hunterPrefab;
    public GameObject monsterPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Called on the server when a client adds a new player with ClientScene.AddPlayer
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        if (conn.connectionId == 0)
        {
            // is host
            player = Instantiate(monsterPrefab) as GameObject;
            Debug.Log("Spawning monster.");
        }
        else
        {
            player = Instantiate(hunterPrefab) as GameObject;
            Debug.Log("Spawning hunter.");
        }
        //GameObject player = Instantiate(Resources.Load(resourcePath, typeof(GameObject))) as GameObject;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
