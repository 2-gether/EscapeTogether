using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MenuInteractable : Interactable {

	[Serializable] public class MenuInteractableClickedEvent : UnityEvent { }

	// Event delegates triggered on click.
	[FormerlySerializedAs("onClick")]
	[SerializeField]
	private MenuInteractableClickedEvent m_OnClick = new MenuInteractableClickedEvent();

	public MenuInteractableClickedEvent onClick {
		get { return m_OnClick; }
		set { m_OnClick = value; }
	}

	public override void Action() {
		m_OnClick.Invoke();
	}

	public override bool CanBeUsed() {
		return true;
	}
}
