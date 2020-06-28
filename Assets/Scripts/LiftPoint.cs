using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPoint : MonoBehaviour {
	[SerializeField] float size = .2f;
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(transform.position, Vector3.one * size);
	}
}
