using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {


	#region PlayerScripts
		PlayerNetwork pn;
		PlayerController pc;
	#endregion

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
		pc = GetComponent<PlayerController>();
		pn = GetComponent<PlayerNetwork>();
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

		if(Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Ground", "Interactable"))) {
			CursorHit = hit;
			isCursorOnScreen = true;
		} else {
			isCursorOnScreen = false;
		}

		//Hover
		if(isCursorOnScreen) {
			GameObject hitObject = CursorHit.collider.gameObject;

			Interactable hitObjectInteractable = hitObject.GetComponent<Interactable>();
			if(CursorHit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable")
				&& hitObjectInteractable.Radius >= Vector3.Distance(CursorHit.point, transform.position)
				&& hitObjectInteractable.CanBeUsed()
				&& Physics.Raycast(transform.position, hitObject.transform.position - transform.position, out RaycastHit infoCheck, float.PositiveInfinity, LayerMask.GetMask("Environment", "Interactable"))
				&& infoCheck.collider.gameObject == hitObject) {
				//Hover object
				interactable = hitObject;
				hitObjectInteractable.SetHover(true);
			} else if(interactable != null) {
				//Reset hover
				interactable.GetComponent<Interactable>().SetHover(false);
				interactable = null;
			}
		}
		
		if (interactable != null && LeftClick) {
			pn.Action(interactable);
		}
		if (RightClick && pc.BoxHolder.transform.childCount == 1) {
			pn.Action(pc.BoxHolder.transform.GetChild(0).gameObject);
		}
	}
	private void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		if(interactable != null)
			Gizmos.DrawLine(transform.position, interactable.transform.position);
	}
}
