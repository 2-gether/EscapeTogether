using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputController : MonoBehaviour {

	MenuController menuController;
	GameObject interactable;
	bool holdClickFire1 = false;
	bool holdClickFire2 = false;

	public string horizontalMovement = "Horizontal";
	public string verticalMovement = "Vertical";
	public string leftClick = "Fire1";
	public string rightClick = "Fire2";

	public float HorizontalDisplacement { get; private set; }
	public float VerticalDisplacement { get; private set; }
	public RaycastHit CursorHit { get; private set; }
	public bool isCursorOnScreen { get; private set; }
	public bool LeftClick { get; private set; } = false;
	public bool RightClick { get; private set; } = false;

	void Start() {
		menuController = GetComponent<MenuController>();
	}

	void Update() {
		HorizontalDisplacement = Input.GetAxis(horizontalMovement);
		VerticalDisplacement = Input.GetAxis(verticalMovement);

		if(Input.GetAxisRaw(leftClick) == 1 && !holdClickFire1) {
			holdClickFire1 = true;
			LeftClick = true;
		} else if(Input.GetAxisRaw(leftClick) == 0) {
			holdClickFire1 = false;
		} else {
			LeftClick = false;
		}

		if(Input.GetAxisRaw(rightClick) == 1 && !holdClickFire2) {
			holdClickFire2 = true;
			RightClick = true;
		} else if(Input.GetAxisRaw(rightClick) == 0) {
			holdClickFire2 = false;
		} else {
			RightClick = false;
		}

		// Cursor
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Interactable"))) {
			CursorHit = hit;
			isCursorOnScreen = true;
		} else {
			isCursorOnScreen = false;
		}

		//Hover
		if(isCursorOnScreen) {
			GameObject hitObject = CursorHit.collider.gameObject;
			Interactable hitObjectInteractable = hitObject.GetComponent<Interactable>();

			//check if it's not the same object
			if(interactable != hitObject
				&& !menuController.IsInAnimation) {
				//Hover object
				if(interactable != null)
					interactable.GetComponent<Interactable>().SetHover(false);
				interactable = hitObject;
				hitObjectInteractable.SetHover(true);
			}
		} else if(interactable != null) {
			interactable.GetComponent<Interactable>().SetHover(false);
			interactable = null;

		}

		if(interactable != null && LeftClick) {
			interactable.GetComponent<Interactable>().Action();
		}
	}
}
