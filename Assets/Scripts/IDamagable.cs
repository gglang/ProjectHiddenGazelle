using UnityEngine;
using System.Collections;

public interface IDamagable {
	bool IsVulnerable();

	bool Damage(float amount);

	float HealthRemaining();
}
