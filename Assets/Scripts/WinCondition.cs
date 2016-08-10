using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinCondition : MonoBehaviour {
	public GameObject HuntersWinGUI;
	public GameObject MonsterWinsGUI;

	private IList<HealthManager> hiveHealths;
	private HealthManager monsterHealth;
	private IList<HealthManager> hunterHealths;

	void Start() {
		hiveHealths = new List<HealthManager>();
		hunterHealths = new List<HealthManager>();
		UpdateWinConditionTracking();
	}

	public void UpdateWinConditionTracking() {
		GameObject[] hunters = GameObject.FindGameObjectsWithTag("Hunter");
		GameObject[] hives = GameObject.FindGameObjectsWithTag("Hive");
		GameObject monster = GameObject.FindGameObjectWithTag("Monster");

		if(hunters != null) {
			foreach(GameObject hunter in hunters) {
				if(hunter != null) {
					HealthManager hp = hunter.GetComponent<HealthManager>();
					if(!hunterHealths.Contains(hp)) {
						hunterHealths.Add(hp);
						hp.OnDeath += CheckHuntersLose;
					}
				}
			}
		}

		if(hives != null) {
			foreach(GameObject hive in hives) {
				if(hive != null) {
					HealthManager hp = hive.GetComponent<HealthManager>();
					if(!hiveHealths.Contains(hp)) {
						hiveHealths.Add(hp);
						hp.OnDeath += CheckMonsterLose;
					}				
				}
			}
		}

		if(monster != null) {
			HealthManager hp = monster.GetComponent<HealthManager>();
			if(monsterHealth == null) {
				monsterHealth = hp;
				hp.OnDeath += CheckMonsterLose;
			}
		}
	}

	public void AddHunter(GameObject player) {
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

	public void AddHive(GameObject hive) {
		HealthManager healthMan = hive.GetComponent<HealthManager>();
		healthMan.OnDeath += CheckMonsterLose;
		hiveHealths.Add(healthMan);
	}

	private void CheckMonsterLose() {
		StartCoroutine(CheckMonsterLoseAfterWait());
	}

	private IEnumerator CheckMonsterLoseAfterWait() {
		// Give time for relevant objects to kill themselves
		yield return new WaitForSeconds(1f);

		// If no more hives, monster loses
		for(int i = hiveHealths.Count - 1; i >= 0; i--) {
			if(hiveHealths[i] == null) {
				hiveHealths.RemoveAt(i);
			}
		}

		if(hiveHealths.Count == 0) {
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
		for(int i = hunterHealths.Count - 1; i >= 0; i--) {
			if(hunterHealths[i] == null) {
				hunterHealths.RemoveAt(i);
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
