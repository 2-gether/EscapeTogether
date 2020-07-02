using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    List<TileIndexMatcher> prefabs = new List<TileIndexMatcher>();
    public void SetHover(bool active) {
        GetComponentInChildren<MeshRenderer>().enabled = active;
    }

    public void AddPrefab(GameObject go, int index) {
        prefabs.Add(new TileIndexMatcher(go, index));
    }
    public void Clear(int index) {
        foreach (var tileIndexMatcher in prefabs) {
            if (tileIndexMatcher.Index == index)
                Destroy(tileIndexMatcher.Prefab);
        }
        prefabs.RemoveAll(item => item.Index == index);
    }


    struct TileIndexMatcher {
        int index;
        GameObject prefab;
        public TileIndexMatcher(GameObject prefab, int index) {
            this.prefab = prefab;
            this.index = index;
        }
        public int Index { get => index; set => index = value; }
        public GameObject Prefab { get => prefab; set => prefab = value; }
    }
}


