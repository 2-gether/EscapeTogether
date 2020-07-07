using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Serializer))]
public class SerializerSaver : MonoBehaviour {

    Serializer serializer;

    void Start() {
        serializer = GetComponent<Serializer>();
    }


    //TODO put to the right place
    void Update() {
        if (Input.GetKeyDown(EditorInputManger.Instance().Save)){
            Save();
        }
    }
    public void Save() {
        var path = EditorUtility.SaveFilePanel("Save the level", "levels", "level"+".xml", "xml");
        if(path.Length !=0) {
            serializer.Serialize(path);
        }
    }
}
