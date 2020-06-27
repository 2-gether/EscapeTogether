using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	[SerializeField] float radius = 3f;

	[SerializeField] GameObject hover;

	public float Radius { get => radius; }

	public void SetHover(bool isHover) {
		if(hover != null)
			hover.SetActive(isHover);
	}

	public virtual void Action() { }

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
