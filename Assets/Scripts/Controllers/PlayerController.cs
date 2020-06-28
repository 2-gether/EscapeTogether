using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(InputController))]
public class PlayerController : MonoBehaviour {

	[SerializeField] float speed;
	new Rigidbody rigidbody;
	InputController input;

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
		input = GetComponent<InputController>();
	}

	void FixedUpdate() {
		//Movement
		Vector3 movement = Vector3.zero;
		movement += Vector3.right * input.HorizontalDisplacement * speed;
		movement += Vector3.forward * input.VerticalDisplacement * speed;
		movement += new Vector3(0, rigidbody.velocity.y, 0);
		rigidbody.velocity = movement;

		//Look
		if(input.isCursorOnScreen) {
			Vector3 normalizedHit = new Vector3(input.CursorHit.point.x, transform.position.y, input.CursorHit.point.z);
			transform.LookAt(normalizedHit);
		}
	}
}
