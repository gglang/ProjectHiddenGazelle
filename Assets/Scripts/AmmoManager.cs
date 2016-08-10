using UnityEngine;
using System.Collections;

public class AmmoManager : MonoBehaviour {

    public int availableAmmo;

    public void putAmmo(int ammo)
    {
        this.availableAmmo += ammo;
    }

    public void useAmmo()
    {
        availableAmmo--;
    }

    public bool hasAmmo()
    {
        return (availableAmmo > 0);
    }

    public int getAmmo()
    {
        return availableAmmo;
    }
}
