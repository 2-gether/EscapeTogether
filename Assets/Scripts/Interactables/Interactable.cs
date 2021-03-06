﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
	[SerializeField] float radius = 3f;
	[SerializeField] protected GameObject hover;

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
	}
}
