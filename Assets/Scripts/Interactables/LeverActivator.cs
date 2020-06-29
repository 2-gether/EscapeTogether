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

		bool canBeUsed = CanBeUsed();
		if(isUp) {
			stick.transform.localEulerAngles = new Vector3(0, 35f, 0);
			hover = hollowUp;
			hollowDown.SetActive(false);
			/*if(canBeUsed)
				hollowUp.SetActive(true);*/
		} else {
			stick.transform.localEulerAngles = new Vector3(0, -35f, 0);
			hover = hollowDown;
			hollowUp.SetActive(false);
			/*if(canBeUsed)
				hollowDown.SetActive(true);*/
		}
	}

	public override bool CanBeUsed() {
		bool targetsReady = true;
		foreach(Actionable a in targets)
			targetsReady &= a.CanBeActioned();
		return !isUsed && targetsReady;
	}
}
