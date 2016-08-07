using UnityEngine;
using System.Collections;

public interface IPurchasable {
	int PurchaseCost();
	bool Purchasable();
	bool Purchase();
}
