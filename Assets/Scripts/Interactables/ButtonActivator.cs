using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkIdentity))]
public class ButtonActivator : Activator {
	[SerializeField] float time = 2f;
	[SerializeField] GameObject ui;
	[SerializeField] Slider uiSlider;

	bool isUsed = false;

	public override void Action(NetworkIdentity player) {
		if(!isUsed) {
			isUsed = true;
			ui.SetActive(isUsed);
			foreach(Actionable a in targets)
				a.Action();
			StartCoroutine(Countdown());
		}
	}

	IEnumerator Countdown() {
		float cd = 0f;
		while(cd < time) {
			float delta = Time.deltaTime;
			cd += delta;
			uiSlider.value = cd / time;
			yield return new WaitForSeconds(delta);
		}
		isUsed = false;
		ui.SetActive(isUsed);
		foreach(Actionable a in targets)
			a.Action();
	}

	public override bool CanBeUsed() {
		bool targetsReady = true;
		foreach(Actionable a in targets)
			targetsReady &= a.CanBeActioned();
		return !isUsed && targetsReady;
	}
}
