using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftActionable : Actionable {
	[SerializeField] Transform lift;
	[SerializeField] List<Transform> steps;
	[SerializeField] float delay = 0.02f;
	[SerializeField] float speed = 1f;
	bool isGoingUp = false;
	bool isActive = false;

	public override void Action() {
		isGoingUp = !isGoingUp;
		StartCoroutine("TransferLift");
	}

	public override bool CanBeActioned() {
		return !isActive;
	}

	IEnumerator TransferLift() {
		isActive = true;
		if(isGoingUp)
			for(int gap = 1; gap < steps.Count; gap++) {
				//take step i-1 and i
				Vector3 from, to;
				from = steps[gap - 1].position;
				to = steps[gap].position;
				float step = Vector3.Distance(from, to) / speed / delay;

				for(int i = 1; i <= step; i++) {
					lift.position = Vector3.Lerp(from, to, i / step);
					yield return new WaitForSeconds(delay);
				}
			}
		else
			for(int gap = steps.Count - 1; gap > 0; gap--) {
				//take step i and i-1
				Vector3 from, to;
				from = steps[gap].position;
				to = steps[gap - 1].position;
				float step = Vector3.Distance(from, to) / speed / delay;

				for(int i = 1; i <= step; i++) {
					lift.position = Vector3.Lerp(from, to, i / step);
					yield return new WaitForSeconds(delay);
				}
			}
		isActive = false;
	}
	void OnDrawGizmosSelected() {
		for(int gap = 0; gap < steps.Count; gap++) {
			if(gap == 0)
				Gizmos.color = Color.blue;
			else if(gap == steps.Count - 1)
				Gizmos.color = Color.red;
			else
				Gizmos.color = Color.yellow;
			Gizmos.DrawCube(steps[gap].position, Vector3.one * .2f);
		}

		Gizmos.color = Color.yellow;
		if(steps.Count >= 2)
			for(int gap = 1; gap < steps.Count; gap++) {
				//take step i-1 and i
				Vector3 from, to;
				from = steps[gap - 1].position;
				to = steps[gap].position;
				Gizmos.DrawLine(from, to);
			}
	}
}
