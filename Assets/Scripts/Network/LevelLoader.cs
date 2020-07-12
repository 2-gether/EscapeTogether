using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LevelLoader : NetworkBehaviour {

    const string PATH = "Levels/";
    [SerializeField] Transform parent;
    Dictionary<string, GameObject> prefabs;
    [SerializeField] GameObject[] gos;

    void Start() {
        Init();
    }

    void Init() {
        prefabs = new Dictionary<string, GameObject>();
        foreach (GameObject pref in gos) {
            prefabs.Add(pref.name, pref);
        }

    }

    public void LoadLevel(string levelName) {
        string path = "Levels/"+levelName+".xml";
        Level l = Serializer.Deserialize<Level>(path);
        foreach (TransformXml pref in l.transforms) {
            prefabs.TryGetValue(pref.prefabName, out GameObject go);
            if (go == null) {
                Debug.LogError(pref.prefabName + "doesn't exist");
            }
            GameObject item = Instantiate(go, new Vector3(pref.x, pref.y, pref.z), Quaternion.Euler(pref.rx, pref.ry, pref.rz), parent);
            item.name = item.name.Substring(0, item.name.Length - "(Clone)".Length);
            NetworkServer.Spawn(item);
        }
    }
}
