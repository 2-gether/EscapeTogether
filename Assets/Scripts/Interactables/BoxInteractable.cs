using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class BoxInteractable : Interactable {
	public override void Action() {
		ActionOnServer();
	}

	[Server]
	private void ActionOnServer() {
		Debug.Log("Hé oui monsieur moua j'y vé bi1.");
	}

	public override bool CanBeUsed() {
		return true;
	}
}
