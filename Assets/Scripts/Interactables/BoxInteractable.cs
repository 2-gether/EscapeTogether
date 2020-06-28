using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class BoxInteractable : Interactable {
	public override void Action() {
		Debug.Log("Boc clicked");
	}

	public override bool CanBeUsed() {
		return true;
	}
}
