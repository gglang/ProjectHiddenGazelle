using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinCondition : MonoBehaviour {
	public GameObject HuntersWinGUI;
	public GameObject MonsterWinsGUI;

	private IList<HiveController> hives;
	private MonsterHealthManager monsterHealth;
	private IList<MonsterHealthManager> hunterHealths;

	void Start() {
		hives = new List<HiveController>();
		hunterHealths = new List<MonsterHealthManager>();
	}

	public void AddPlayer(MonsterHealthManager player) {
		player.OnDeath += CheckHuntersLose;
		hunterHealths.Add(player);
	}

	public void AddMonster(MonsterHealthManager monster) {
		monster.OnDeath += CheckMonsterLose;
		monsterHealth = monster;
	}

	public void AddHive(HiveController hive) {
		hive.OnDeath += CheckMonsterLose;
		hives.Add(hive);
	}

	private void CheckMonsterLose() {
		StartCoroutine(CheckMonsterLoseAfterWait());
	}

	private IEnumerator CheckMonsterLoseAfterWait() {
		// Give time for relevant objects to kill themselves
		yield return new WaitForSeconds(1f);

		// If no more hives, monster loses
		for(int i = hives.Count; i > 0; i--) {
			if(hives[i] == null) {
				hives.Remove(hives[i]);
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
