using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class LeverActivator : Activator {

	[SerializeField] bool isMultiUsage = false;
	[SerializeField] GameObject hollowUp, hollowDown, stick;
	bool isUsed = false;
	bool isUp = true;

	public override void Action(NetworkIdentity player) {
		isUp = !isUp;
		if(!isUsed || isMultiUsage) {
			if(!isMultiUsage)
				isUsed = true;
			foreach(Actionable a in targets)
				a.Action();
		}

		if(isUp) {
			stick.transform.localEulerAngles = new Vector3(0, 35f, 0);
			hover = hollowUp;
			hollowDown.SetActive(false);
			hollowUp.SetActive(true);
		} else {
			stick.transform.localEulerAngles = new Vector3(0, -35f, 0);
			hover = hollowDown;
			hollowDown.SetActive(true);
			hollowUp.SetActive(false);
		}
	}

	public override bool CanBeUsed() {
		bool targetsReady = true;
		foreach(Actionable a in targets)
			targetsReady &= a.CanBeActioned();
		return !isUsed && targetsReady;
	}
}
