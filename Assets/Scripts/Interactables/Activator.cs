using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activator : NetworkInteractable {
	[SerializeField] protected List<Actionable> targets;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		foreach(Actionable a in targets)
			if(a != null)
				Gizmos.DrawLine(a.gameObject.transform.position, transform.position);
	}
}
