using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour {

	new Rigidbody rigidbody;
	GameObject interactable;
	[SerializeField] float speed;

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
	}

	public override void OnStartLocalPlayer() {
		base.OnStartLocalPlayer();
		if (hasAuthority) {
			Camera cam = Camera.main;
			cam.GetComponent<CameraController>().Player = this.transform;
		}
	}

	void FixedUpdate() {
		//Movement
		Vector3 movement = Vector3.zero;
		movement += Vector3.right * Input.GetAxis("Horizontal") * speed;
		movement += Vector3.forward * Input.GetAxis("Vertical") * speed;
		rigidbody.velocity = movement;

		//Look
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit)) {
			Vector3 normalizedHit = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			transform.LookAt(normalizedHit);
		}

	}

	private void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit)) {
			GameObject hitObject = hit.collider.gameObject;

			if(interactable != hitObject) {
				if(hit.collider.tag == "Interactable"
					&& hitObject.GetComponent<Interactable>().Radius >= Vector3.Distance(hit.point, transform.position)) {
					//Hover object
					interactable = hitObject;
					interactable.GetComponent<Interactable>().SetHover(true);
				} else if(interactable != null) {
					//Reset hover
					interactable.GetComponent<Interactable>().SetHover(false);
					interactable = null;
				}
			}
		}
	}
}
