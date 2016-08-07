using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class NetworkManagerHostIsUnique : NetworkManager {

    public GameObject hunterPrefab;
    public GameObject monsterPrefab;
    public GameObject monsterCameraPrefab;
	public GameObject hivePrefab;

    public Vector3 monsterSpawnPosition;
	public Vector3 hiveSpawnPosition;
    public Vector3 hunterSpawnPosition;

    public GameObject gameManager;

    // Called on the server when a client adds a new player with ClientScene.AddPlayer
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (conn.connectionId == 0)
        {
            // is host
            GameObject monster = Instantiate(monsterPrefab) as GameObject;
			GameObject.Find("GameManager").GetComponent<WinCondition>().AddMonster(monster);
			Instantiate(hivePrefab, hiveSpawnPosition, Quaternion.identity);
            GameObject monsterCamera = Instantiate(monsterCameraPrefab) as GameObject;
            FreeLookCam freeLookCam = monsterCamera.GetComponent<FreeLookCam>();
            Debug.Log("Spawning monster.");
            NetworkServer.Spawn(monsterCamera);
            NetworkServer.AddPlayerForConnection(conn, monster, playerControllerId);
            freeLookCam.SetTarget(monster.transform);
            monster.transform.position = monsterSpawnPosition;
            monsterCamera.transform.position = monsterSpawnPosition;
        }
        else
        {
            GameObject hunter = Instantiate(hunterPrefab) as GameObject;
			GameObject.Find("GameManager").GetComponent<WinCondition>().AddMonster(hunter);
            Debug.Log("Spawning hunter.");
            NetworkServer.AddPlayerForConnection(conn, hunter, playerControllerId);
            hunter.transform.position = hunterSpawnPosition;
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
		SpawnResources[] spawners = gameManager.GetComponents<SpawnResources>();
		foreach(SpawnResources spawnee in spawners) {
			spawnee.PopulateTheMasses();
		}
        //SpawnResources spawnResources = GetComponent<SpawnResources>();
        //spawnResources.Spawn();
    }
}
