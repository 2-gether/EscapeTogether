﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activator : Interactable {
	[SerializeField] protected List<Actionable> targets;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		foreach(Actionable a in targets)
			Gizmos.DrawLine(a.gameObject.transform.position, transform.position);
	}
}