using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] Vector3 offset;
	[SerializeField] Transform player;
	public Transform Player => player;

	void FixedUpdate() {
		transform.position = player.position + offset;
	}
}
