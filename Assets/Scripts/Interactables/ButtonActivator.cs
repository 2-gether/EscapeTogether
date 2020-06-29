using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : Activator {
	[SerializeField] float time = 2f;
	bool isUsed = false;

	public override void Action(NetworkIdentity player) {
		if(!isUsed) {
			isUsed = true;
			foreach(Actionable a in targets)
				a.Action();
			StartCoroutine(Countdown());
		}
	}

	IEnumerator Countdown() {
		yield return new WaitForSeconds(time);
		isUsed = false;
		foreach(Actionable a in targets)
			a.Action();
	}

	public override bool CanBeUsed() {
		bool targetsReady = true;
		foreach(Actionable a in targets)
			targetsReady &= a.CanBeActioned();
		return !isUsed && targetsReady;
	}
}
