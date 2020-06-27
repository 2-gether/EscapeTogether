using UnityEngine;
using Mirror;
public class PlayerNetwork : NetworkBehaviour {

    [SerializeField] Behaviour[] toDisable;
    public override void OnStartLocalPlayer() {
        if (hasAuthority) {
            Camera cam = Camera.main;
            cam.GetComponent<CameraController>().Player = transform;
        }
    }
    public override void OnStartClient() {
    
        if (!hasAuthority) {
            foreach (Behaviour b in toDisable)
                b.enabled = false;
        }
    }
    [Command]
    public void CmdAction(NetworkIdentity id) {
        RpcAction(id);
    }

    [ClientRpc]
    public void RpcAction(NetworkIdentity id) {
        id.GetComponent<Interactable>().Action();
    }
}
