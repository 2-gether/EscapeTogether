using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	[SerializeField] float animationSpeed = 5f;
	[SerializeField] GameObject[] menus;
	bool isInAnimation = false;
	public bool IsInAnimation => isInAnimation;

	int currentIndex = 0;

	public void GoToMenu(int index) {
		Transform from = menus[currentIndex].transform.Find("CameraWaypoint");
		Transform to = menus[index].transform.Find("CameraWaypoint");
		StartCoroutine(AnimationTransferMenu(from, to));
		currentIndex = index;
	}

	IEnumerator AnimationTransferMenu(Transform from, Transform to) {
		isInAnimation = true;

		float delay = 0.02f;
		Vector3 fromPos, toPos;
		Quaternion fromRot, toRot;

		fromPos = from.position;
		toPos = to.position;
		fromRot = from.rotation;
		toRot = to.rotation;

		float pas = Vector3.Distance(fromPos, toPos) / animationSpeed / delay;
		for(int i = 1; i <= pas; i++) {
			transform.position = Vector3.Lerp(fromPos, toPos, i / pas);
			transform.rotation = Quaternion.Slerp(fromRot, toRot, i / pas);
			yield return new WaitForSeconds(delay);
		}

		transform.position = toPos;
		transform.rotation = toRot;

		isInAnimation = false;
	}
}
