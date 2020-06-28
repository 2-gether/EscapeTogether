using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class ButtonInteractable : Interactable {

	[SerializeField] bool isMultiUsage = false;
	bool isUsed = false;

	public override void Action() {
		if(!isUsed || isMultiUsage) {
			if(!isMultiUsage)
				isUsed = true;
			foreach(Actionable a in targets)
				a.Action();
		}
	}

	public override bool CanBeUsed() {
		bool targetsReady = true;
		foreach(Actionable a in targets)
			targetsReady = targetsReady && a.CanBeActioned();
		return !isUsed && targetsReady;
	}
}
