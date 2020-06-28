using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(NetworkTransform))]
public class BoxInteractable : Interactable {
	bool isCoroutineRuning = false;

	public override void Action(NetworkIdentity player) {
		ActionOnServer(player);
	}

	[Server]
	void ActionOnServer(NetworkIdentity id) {
		RpcActionClients(id);
	}

	[ClientRpc]
	void RpcActionClients(NetworkIdentity id) {
		IEnumerator coroutine;
		if(transform.parent == null) {
			coroutine = Animation(id, true);
		} else {
			coroutine = Animation(id, false);
		}
		if(isCoroutineRuning)
			StopCoroutine("Animation");
		StartCoroutine(coroutine);
	}

	IEnumerator Animation(NetworkIdentity id, bool isPickingUp) {
		isCoroutineRuning = true;
		float delay = 0.02f;
		Vector3 fromPos, toPos;
		Vector3 fromScale, toScale;

		if(isPickingUp) {
			transform.SetParent(id.GetComponent<PlayerController>().BoxHolder);
			transform.GetComponent<BoxCollider>().isTrigger = true;
			transform.GetComponent<Rigidbody>().isKinematic = true;
			transform.localRotation = Quaternion.identity;

			fromPos = transform.localPosition;
			toPos = Vector3.zero;
			fromScale = transform.localScale;
			toScale = Vector3.one * 0.8f;

		} else {
			transform.SetParent(null);

			fromPos = id.GetComponent<PlayerController>().BoxHolder.position;
			toPos = id.transform.localPosition + id.transform.forward * 1f + Vector3.up;
			fromScale = transform.localScale;
			toScale = Vector3.one * 0.8f;
		}

		float pas = Vector3.Distance(fromPos, toPos) / 10f / delay * (transform.parent != null ? transform.parent.localScale.x : 1);
		for(int i = 1; i <= pas; i++) {
			transform.localPosition = Vector3.Lerp(fromPos, toPos, i / pas);
			transform.localScale = Vector3.Lerp(fromScale, toScale, i / pas);
			yield return new WaitForSeconds(delay);
		}

		if(!isPickingUp) {
			transform.GetComponent<BoxCollider>().isTrigger = false;
			Rigidbody boxRb = transform.GetComponent<Rigidbody>();
			boxRb.isKinematic = false;
			boxRb.velocity = Vector3.zero;
		}
		transform.localPosition = toPos;
		transform.localScale = toScale;

		isCoroutineRuning = false;
	}

	public override bool CanBeUsed() {
		return true;
	}
}
