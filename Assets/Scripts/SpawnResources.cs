using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnResources : MonoBehaviour {
	public int SpawnCount = 1000;
	public GameObject ResourceToSpawn;

	public void PopulateTheMasses () {
		StartCoroutine(DumbSpawnCuzFail());
	}

	private IEnumerator DumbSpawnCuzFail() {
		yield return new WaitForSeconds(2f);
		int i = 0;
		while(i < SpawnCount) {
			Vector3 spawnLocation = new Vector3(Utilities.TrueRandomRange(0f,1500f), 210f, Utilities.TrueRandomRange(0f,1500f));
			float randomAngle = Utilities.TrueRandomRange(0f, 360f);
			GameObject resource = Instantiate(ResourceToSpawn, spawnLocation, Quaternion.Euler(new Vector3(randomAngle, randomAngle, randomAngle))) as GameObject;
			NetworkServer.Spawn(resource);
			i++;
		}
	}
}
