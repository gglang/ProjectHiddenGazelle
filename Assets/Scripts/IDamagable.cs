using UnityEngine;
using System.Collections;

public interface IDamagable {
	/// <summary>
	/// Damage the specified amount. Return true if damage went through, false otherwise.
	/// </summary>
	/// <param name="amount">Amount.</param>
	bool Damage(float amount);

	float HealthRemaining();
}
