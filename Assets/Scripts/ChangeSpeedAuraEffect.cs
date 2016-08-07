using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

[RequireComponent(typeof(TrackInsideTaggedTriggers))]
public class ChangeSpeedAuraEffect : AbEffect {
	public float ChangeSpeedFraction = 1f;
	public string AuraObjectTag;
	private bool effectApplied = false;
	private TrackInsideTaggedTriggers auraTracker;

	override protected void ActivateEffect() {
		auraTracker = GetComponent<TrackInsideTaggedTriggers>();

		if(ChangeSpeedFraction <= 0) {
			Destroy(this.gameObject);
			throw new ArgumentOutOfRangeException("Change speed effects cannot be <= 0");
		}

		FirstPersonController fps = GetComponentInParent<FirstPersonController>();
		ThirdPersonCharacter tps = GetComponentInParent<ThirdPersonCharacter>();
		if(fps != null) {
			fps.ChangeSpeed(ChangeSpeedFraction);
			effectApplied = true;
		} else if(tps != null) {
			tps.ChangeSpeed(ChangeSpeedFraction);
			effectApplied = true;
		}

		StartCoroutine(CheckAuraStatusLoop());
	}

	override protected void DisactivateEffect() {
		if(ChangeSpeedFraction <= 0) {
			return;
		}

		if(!effectApplied) {
			effectApplied = false;
		}

		// This effect assumes it was instantiated as a child of whatever it is effecting.
		FirstPersonController fps = GetComponentInParent<FirstPersonController>();
		ThirdPersonCharacter tps = GetComponentInParent<ThirdPersonCharacter>();
		GameObject parent = this.transform.parent.gameObject;

		if(fps != null) {
			fps.ChangeSpeed(1f/ChangeSpeedFraction);
			effectApplied = false;
		} else if(tps != null) {
			tps.ChangeSpeed(1f/ChangeSpeedFraction);
			effectApplied = false;
		}
	}

	private IEnumerator CheckAuraStatusLoop() {
		while(true) {
			yield return new WaitForSeconds(0.5f);
			if(!auraTracker.InsideTaggedTriggers()) {
				Destroy(this.gameObject);
				break;
			}
		}
	}
}
