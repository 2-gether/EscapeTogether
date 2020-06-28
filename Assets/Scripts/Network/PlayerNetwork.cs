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

    public void Action(GameObject go) {
        CmdAction(go.GetComponent<NetworkIdentity>(),GetComponent<NetworkIdentity>());
    }
    [Command]
    private void CmdAction(NetworkIdentity id,NetworkIdentity player) {
        RpcAction(id,player);
    }

    [ClientRpc]
    private void RpcAction(NetworkIdentity id, NetworkIdentity player) {
        id.GetComponent<Interactable>().Action(player);
    }
}
