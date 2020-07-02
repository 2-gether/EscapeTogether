using UnityEngine;
using System.Collections;

public class FileSystemFile : MonoBehaviour {
    [SerializeField] GameObject[] prefab;
    public GameObject[] Prefab { get => prefab; set => prefab = value; }

    public void Click() {
        SetPreview();
    }

    void SetPreview() {
        EditorManager.Instance.CurrentFile = this;
        EditorManager.Instance.Preview = GetPrefab(0);
    }

    public GameObject GetPrefab(int index) {
        if (EditorManager.Instance.CurrentTile == null)
            return Instantiate(prefab[index % prefab.Length]);
        else return Instantiate(prefab[index % prefab.Length], EditorManager.Instance.CurrentTile.transform.position, Quaternion.identity); ;

    }
}
