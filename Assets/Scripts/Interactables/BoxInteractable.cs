﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(NetworkTransform))]
public class BoxInteractable : Interactable {
	public override void Action(NetworkIdentity player) {
		ActionOnServer(player);
	}

	[Server]
	void ActionOnServer(NetworkIdentity id) {
		RpcActionClients(id);
	}

	[ClientRpc]
	void RpcActionClients(NetworkIdentity id) {
		if(transform.parent == null) {
			transform.SetParent(id.GetComponent<PlayerController>().BoxHolder, false);
			transform.localPosition = new Vector3(0, 0, 0);
			transform.localRotation = Quaternion.identity;
			transform.GetComponent<Rigidbody>().isKinematic = true;
			transform.GetComponent<BoxCollider>().enabled = false;
		} else {
			transform.SetParent(null);
			// get the position forward the player.
			transform.position = id.transform.position + id.transform.forward * 1f + Vector3.up;
			transform.localScale = Vector3.one * .8f;
			transform.GetComponent<BoxCollider>().enabled = true;
			Rigidbody boxRb = transform.GetComponent<Rigidbody>();
			boxRb.isKinematic = false;
			boxRb.velocity = Vector3.zero;

		}
	}

	public override bool CanBeUsed() {
		return true;
	}
}
