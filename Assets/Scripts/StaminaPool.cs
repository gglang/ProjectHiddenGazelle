using UnityEngine;
using System.Collections;

public class StaminaPool : MonoBehaviour {
	public float MaxStamina = 100f;

	public float currentStamina;

	private UnityEngine.UI.Text guiText;

	void Start () {
//		currentStamina = MaxStamina;
		currentStamina = MaxStamina * 0.5f;
		GameObject staminaText = GameObject.Find("StaminaText");
		if(staminaText != null) {
			guiText = staminaText.GetComponent<UnityEngine.UI.Text>();
		}
	}

	void Update() {
		if(guiText != null) {
			guiText.text = "Stamina: "+currentStamina;
		}
	}

	/// <summary>
	/// Changes the stamina. Negative input lowers stamina.
	/// </summary>
	/// <returns><c>true</c>, if stamina was changed, <c>false</c> otherwise.</returns>
	/// <param name="amount">Amount.</param>
	public bool ChangeStamina(float amount) {
		if((currentStamina + amount) < 0) {
			return false;
		} else if(currentStamina + amount > MaxStamina) {
			currentStamina = MaxStamina;
		} else {
			currentStamina += amount;
		}
		return true;
	}
}
