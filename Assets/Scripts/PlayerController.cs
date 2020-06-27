using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody), typeof(InputController))]
public class PlayerController : NetworkBehaviour {

	[SerializeField] float speed;
	new Rigidbody rigidbody;
	GameObject interactable;
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
		rigidbody.velocity = movement;

		//Look
		if(input.isCursorOnScreen) {
			Vector3 normalizedHit = new Vector3(input.CursorHit.point.x, transform.position.y, input.CursorHit.point.z);
			transform.LookAt(normalizedHit);
		}
	}

	void Update() {
		if(input.isCursorOnScreen) {
			GameObject hitObject = input.CursorHit.collider.gameObject;

			Interactable hitObjectInteractable = hitObject.GetComponent<Interactable>();
			if(input.CursorHit.collider.tag == "Interactable"
				&& hitObjectInteractable.Radius >= Vector3.Distance(input.CursorHit.point, transform.position)
				&& hitObjectInteractable.CanBeUsed()) {
				//Hover object
				interactable = hitObject;
				hitObjectInteractable.SetHover(true);
			} else if(interactable != null) {
				//Reset hover
				interactable.GetComponent<Interactable>().SetHover(false);
				interactable = null;
			}
		}

		//TODO Network
		if(interactable != null && input.Click) {
			GetComponent<PlayerNetwork>().CmdAction(interactable.GetComponent<NetworkIdentity>());
		}
	}
}
