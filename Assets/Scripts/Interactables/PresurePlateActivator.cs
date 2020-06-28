using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class PresurePlateActivator : Activator {
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" || other.tag == "Box") {
            Activate(other.GetComponent<NetworkIdentity>());
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player" || other.tag == "Box") {
            Activate(other.GetComponent<NetworkIdentity>());
        }
    }

    public override void Action(NetworkIdentity player) {
        foreach (Actionable a in targets)
            if (a.CanBeActioned())
                a.Action();
    }

    public override bool CanBeUsed() {
        return false;
    }


    [Server]
    void Activate(NetworkIdentity id) {
        RpcActivate(id);
    }

    [ClientRpc]
    void RpcActivate(NetworkIdentity id) {
        Action(id);
    }
}
