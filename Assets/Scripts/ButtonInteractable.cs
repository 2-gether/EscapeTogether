using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Interactable {

	[SerializeField] bool isMultiUsage = false;
	bool isUsed = false;
	[SerializeField] List<Actionable> targets;

	public override void Action() {
		if(!isUsed || isMultiUsage) {
			if(!isMultiUsage)
				isUsed = true;
			foreach(Actionable a in targets)
				a.Action();
		}
	}

	public override bool CanBeUsed() {
		return !isUsed;
	}
}
