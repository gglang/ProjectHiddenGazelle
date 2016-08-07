using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinCondition : MonoBehaviour {
	public GameObject HuntersWinGUI;
	public GameObject MonsterWinsGUI;

	private IList<HiveController> hives;
	private HealthManager monsterHealth;
	private IList<HealthManager> hunterHealths;

	void Start() {
		hives = new List<HiveController>();
		hunterHealths = new List<HealthManager>();
	}

	public void AddPlayer(GameObject player) {
		HealthManager healthMan = player.GetComponent<HealthManager>();
		if(healthMan == null) {
			return;
		}

		healthMan.OnDeath += CheckHuntersLose;
		hunterHealths.Add(healthMan);
	}

	public void AddMonster(GameObject monster) {
		HealthManager healthMan = monster.GetComponent<HealthManager>();
		if(healthMan == null) {
			return;
		}

		healthMan.OnDeath += CheckMonsterLose;
		monsterHealth = healthMan;
	}

	public void AddHive(HiveController hive) {
		hive.BeforeDeath += CheckMonsterLose;
		hives.Add(hive);
	}

	private void CheckMonsterLose() {
		StartCoroutine(CheckMonsterLoseAfterWait());
	}

	private IEnumerator CheckMonsterLoseAfterWait() {
		// Give time for relevant objects to kill themselves
		yield return new WaitForSeconds(1f);

		// If no more hives, monster loses
		for(int i = hives.Count - 1; i >= 0; i--) {
			if(hives[i] == null) {
				hives.RemoveAt(i);
			}
		}

		if(hives.Count == 0) {
			MonsterLose();
		}

		// If monster died, monster loses
		if(monsterHealth == null) {
			MonsterLose();
		}
	}

	private void MonsterLose() {
		Debug.Log("HUNTERS WIN!");
		if(HuntersWinGUI != null) {
			HuntersWinGUI.SetActive(true);
		}
	}

	private void CheckHuntersLose() {
		StartCoroutine(CheckHuntersLoseAfterWait());
	}

	private IEnumerator CheckHuntersLoseAfterWait() {
		// Give time for relevant objects to kill themselves
		yield return new WaitForSeconds(1f);

		// If all players dead, players lose
		for(int i = hunterHealths.Count; i > 0; i--) {
			if(hunterHealths[i] == null) {
				hunterHealths.Remove(hunterHealths[i]);
			}
		}

		if(hunterHealths.Count == 0) {
			HuntersLose();
		}
	}

	private void HuntersLose() {
		Debug.Log("MONSTER WINS!");
		if(MonsterWinsGUI != null) {
			MonsterWinsGUI.SetActive(true);
		}
	}
}
