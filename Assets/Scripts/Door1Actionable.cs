using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Actionable : Actionable {
	[SerializeField] GameObject door;
	bool isOpen = false;
	public override void Action() {
		Debug.Log("Door1Action");
		isOpen = !isOpen;
		if(door != null)
			door.SetActive(!isOpen);
	}
}
