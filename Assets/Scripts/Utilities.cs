using UnityEngine;
using System.Collections;

public class Utilities {
	
	/// <summary>
	/// Returns a random int that is actually random (unlike UnityEngine.Random).
	///	Uniform distribution. Range is from min (inclusive) to max (inclusive).
	/// </summary>
	/// <returns>The random range.</returns>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Max.</param>
	public static float TrueRandomRange(float min, float max) {
		float randomValue = UnityEngine.Random.value;
		return Mathf.Lerp(min, max, randomValue);
	}

	/// <summary>
	/// Returns a random float that is actually random (unlike UnityEngine.Random).
	/// Uniform distribution. Range is from min (inclusive) to max (exclusive).
	/// </summary>
	/// <returns>The random range.</returns>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Max.</param>
	public static int TrueRandomRange(int min, int max) {
		if(min == max) {
			return min;
		}

		int myMin;
		int myMax;
		if(min > max) {
			myMax = min;
			myMin = max;
		} else {
			myMax = max;
			myMin = min;
		}

		float randomValue = UnityEngine.Random.value;
		int result = (int) Mathf.Lerp((float) myMin, (float) myMax, randomValue);	// Rounds down
		if(result == max) {
			result = max - 1;
		}
		return result;
	}
}
