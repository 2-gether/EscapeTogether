using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager {
    public override void OnServerSceneChanged(string sceneName) {
        base.OnServerSceneChanged(sceneName);

        ChangeLevel();
    }



    public void ChangeLevel() {
        Debug.Log("ChangeLevel");
        // Load loading screen for all players. Desactivate player movement ... 

        // Remove all transform in the parent container.

        // Get the level prefabs. 

        // Load the level 

        // Spawn the prefabs

        // Unload loading screen for all player. Activate player movement 



        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadLevel("level");


    }
}
