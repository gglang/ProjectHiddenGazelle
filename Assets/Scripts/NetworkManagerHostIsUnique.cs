using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class NetworkManagerHostIsUnique : NetworkManager {

    public GameObject hunterPrefab;
    public GameObject monsterPrefab;
    public GameObject monsterCameraPrefab;

    public Vector3 monsterSpawnPosition;
    public Vector3 hunterSpawnPosition;

    public GameObject gameManager;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Called on the server when a client adds a new player with ClientScene.AddPlayer
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (conn.connectionId == 0)
        {
            // is host
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
            GameObject monsterCamera = Instantiate(monsterCameraPrefab) as GameObject;
            FreeLookCam freeLookCam = monsterCamera.GetComponent<FreeLookCam>();
            freeLookCam.SetTarget(monster.transform);
            Debug.Log("Spawning monster.");
            NetworkServer.Spawn(monsterCamera);
            NetworkServer.AddPlayerForConnection(conn, monster, playerControllerId);
            monster.transform.position = monsterSpawnPosition;
            monsterCamera.transform.position = monsterSpawnPosition;
        }
        else
        {
            GameObject hunter = Instantiate(hunterPrefab) as GameObject;
            Debug.Log("Spawning hunter.");
            NetworkServer.AddPlayerForConnection(conn, hunter, playerControllerId);
            hunter.transform.position = hunterSpawnPosition;
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        //SpawnResources spawnResources = GetComponent<SpawnResources>();
        //spawnResources.Spawn();
    }
}
