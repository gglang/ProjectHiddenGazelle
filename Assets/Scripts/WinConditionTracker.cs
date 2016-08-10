using UnityEngine;
using System.Collections;

public class WinConditionTracker : MonoBehaviour {
	public WinConditions myCondition;

	public enum WinConditions { Monster, Hunter, Hive };

	void Start () {
		WinCondition winConditionManager = GameObject.Find("GameManager").GetComponent<WinCondition>();
		switch(myCondition) {
		case WinConditions.Hive:
			winConditionManager.AddHive(this.gameObject);
			break;
		case WinConditions.Hunter:
			winConditionManager.AddHunter(this.gameObject);
			break;
		case WinConditions.Monster:
			winConditionManager.AddMonster(this.gameObject);
			break;
		default:
			Debug.LogError("Unknown WinConditions");
			break;
		}
	}
}
