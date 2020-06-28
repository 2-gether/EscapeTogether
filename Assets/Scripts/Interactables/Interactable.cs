using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public abstract class Interactable : MonoBehaviour {
	[SerializeField] float radius = 3f;
	[SerializeField] GameObject hover;
	[SerializeField] protected List<Actionable> targets;

	public float Radius => radius;

	public void SetHover(bool isHover) {
		if(hover != null)
			hover.SetActive(isHover);
	}

	public abstract void Action();
	public abstract bool CanBeUsed();

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
		Gizmos.color = Color.red;
		foreach(Actionable a in targets)
			Gizmos.DrawLine(a.gameObject.transform.position, transform.position);
	}
}
