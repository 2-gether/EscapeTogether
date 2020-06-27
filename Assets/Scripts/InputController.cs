using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	bool holdClick = false;

	public string horizontalMovement = "Horizontal";
	public string verticalMovement = "Vertical";
	public string click = "Fire1";

	public float HorizontalDisplacement { get; private set; }
	public float VerticalDisplacement { get; private set; }
	public RaycastHit CursorHit { get; private set; }
	public bool isCursorOnScreen { get; private set; }
	public bool Click { get; private set; } = false;

	void Update() {
		HorizontalDisplacement = Input.GetAxis(horizontalMovement);
		VerticalDisplacement = Input.GetAxis(verticalMovement);

		if(Input.GetAxisRaw(click) == 1 && !holdClick) {
			holdClick = true;
			Click = true;
		} else if(Input.GetAxisRaw(click) == 0) {
			holdClick = false;
		} else {
			Click = false;
		}

		// Cursor
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit)) {
			CursorHit = hit;
			isCursorOnScreen = true;
		} else {
			isCursorOnScreen = false;
		}
	}
}
