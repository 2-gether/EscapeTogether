using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class PresurePlateActivator : Activator {
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerNetwork>().Action(gameObject);
        }
        else if (other.tag == "Box") {
            other.GetComponent<BoxInteractable>().Action();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerNetwork>().Action(gameObject);
        }
        else if (other.tag == "Box") {
            other.GetComponent<BoxInteractable>().Action();
        }
    }

    public override void Action() {
        foreach (Actionable a in targets)
            if (a.CanBeActioned())
                a.Action();
    }

    public override bool CanBeUsed() {
        return false;
    }
}
