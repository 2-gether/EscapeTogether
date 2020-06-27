using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlateInteractable : Interactable {
	[SerializeField] List<Actionable> targets;

	private void OnTriggerEnter(Collider other) {
		Debug.Log(other.tag);
		if(other.tag == "Player") {
			other.GetComponent<PlayerNetwork>().Action(gameObject);
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<PlayerNetwork>().Action(gameObject);
		}
	}

	public override void Action() {
		foreach(Actionable a in targets)
			a.Action();
	}

	public override bool CanBeUsed() {
		return false;
	}
}
