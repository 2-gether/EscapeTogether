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
	public Transform eyesPos;
	[SerializeField] float rangeDrop = 2f;

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

		if(Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Ground", "Environment", "Interactable"))) {
			CursorHit = hit;
			isCursorOnScreen = true;
		} else {
			isCursorOnScreen = false;
		}

		//Hover
		if(isCursorOnScreen) {
			GameObject hitObject = CursorHit.collider.gameObject;
			NetworkInteractable hitObjectInteractable = hitObject.GetComponent<NetworkInteractable>();

			if(interactable != hitObject) {
				//Check not holding box
				if(pc.BoxHolder.transform.childCount == 0
					//not wrong layer
					&& hitObject.layer == LayerMask.NameToLayer("Interactable")
					//cast the double check
					&& Physics.Raycast(eyesPos.position, hitObject.transform.position - eyesPos.position, out RaycastHit infoCheck, float.PositiveInfinity, LayerMask.GetMask("Ground", "Environment", "Interactable"))
					//double check
					&& infoCheck.collider.gameObject == hitObject
					//object can be used
					&& hitObjectInteractable.CanBeUsed()
					//in the range
					&& hitObjectInteractable.Radius >= Vector3.Distance(CursorHit.point, transform.position)) {
					//Hover object
					if(interactable != null)
						interactable.GetComponent<NetworkInteractable>().SetHover(false);
					interactable = hitObject;
					hitObjectInteractable.SetHover(true);
				} else if(interactable != null) {
					//Reset hover
					interactable.GetComponent<NetworkInteractable>().SetHover(false);
					interactable = null;
				}
			} else {
				if(!hitObjectInteractable.CanBeUsed())
					hitObjectInteractable.SetHover(false);
				else
					hitObjectInteractable.SetHover(true);
			}
		}

		if(interactable != null && LeftClick) {
			pn.Action(interactable);
		}
		if(RightClick
			&& pc.BoxHolder.transform.childCount == 1
			&& !Physics.Raycast(eyesPos.position, CursorHit.point - eyesPos.position, out RaycastHit checkDrop, rangeDrop, LayerMask.GetMask("Environment"))) {
			pn.Action(pc.BoxHolder.transform.GetChild(0).gameObject);
		}
	}
	private void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		if(interactable != null)
			Gizmos.DrawLine(eyesPos.position, interactable.transform.position);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(eyesPos.position, eyesPos.position + (CursorHit.point - eyesPos.position).normalized * rangeDrop);
	}
}
