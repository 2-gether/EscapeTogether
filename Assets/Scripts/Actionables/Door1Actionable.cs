using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Actionable : Actionable {
	[SerializeField] GameObject door;

	public override void Action() {
		if(door != null)
			door.SetActive(!door.activeSelf);
	}

	public override bool CanBeActioned() {
		return true;
	}
}
