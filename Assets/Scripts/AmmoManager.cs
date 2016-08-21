using UnityEngine;
using System.Collections;

public class AmmoManager : MonoBehaviour {

    public float availableAmmo;

	private UnityEngine.UI.Text guiText;

	void Start () {
		//		currentStamina = MaxStamina;
		availableAmmo = 100000;
		GameObject staminaText = GameObject.Find("StaminaText");
		if(staminaText != null) {
			guiText = staminaText.GetComponent<UnityEngine.UI.Text>();
		}
		UpdateAmmoUI();
	}

    void Update ()
    {
        availableAmmo = Mathf.Clamp(availableAmmo, 0f, availableAmmo);
    }

	public void UpdateAmmoUI() {
		if(guiText != null) {
			guiText.text = "Ammo: "+availableAmmo;
		}
	}

    public void putAmmo(float ammo)
    {
        this.availableAmmo += ammo;
		UpdateAmmoUI();
    }

    public void useAmmo()
    {
        availableAmmo--;
		UpdateAmmoUI();
    }

    public bool hasAmmo()
    {
        return (availableAmmo > 0);
    }

    public float getAmmo()
    {
        return availableAmmo;
    }
}
