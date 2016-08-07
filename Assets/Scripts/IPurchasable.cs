using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public interface IPurchasable {
	int PurchaseCost();
	bool Purchasable();

    [Command]
	void CmdPurchase();
}
