using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Text.RegularExpressions;

public class CustomNetworkHUD : MonoBehaviour {

    NetworkManager manager;

    void Awake() {
        manager = GetComponent<NetworkManager>();
    }

    public void Join(TextMeshProUGUI ip) {

        if (ip.text.ToLower() == "localhost") manager.networkAddress = ip.text.ToLower();
        else {
            Regex ipFormated = new Regex(@"\b\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}\b");
            MatchCollection result = ipFormated.Matches(ip.text);
            manager.networkAddress = result[0].ToString();
        }
        manager.StartClient();
        Init();
    }

    public void Host() {
        manager.StartHost();
        Init();
    }

    public void Server() {
        manager.StartServer();
        Init();
    }

    public void Exit() {
        Application.Quit();
    }

    void Init() {
        if (NetworkClient.isConnected && !ClientScene.ready) {
            ClientScene.Ready(NetworkClient.connection);

            if (ClientScene.localPlayer == null) {
                ClientScene.AddPlayer(NetworkClient.connection);
            }
        }
    }
}
