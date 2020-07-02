using UnityEngine;

public class EditorStateNormal : EditorState {
    EditorManager editor = EditorManager.Instance;
    public override void Update() {
        if (Input.GetKeyDown(EditorInputManger.Instance().RemovePrefab)) {
            editor.CurrentTile.Clear(editor.LayerIndex);
        }

        if (Input.GetMouseButtonDown(0)) {
            if (editor.CurrentTile != null && editor.Preview != null)
                editor.CurrentTile.AddPrefab(editor.InstantiatePrefab(), editor.LayerIndex);
            else {
                if (editor.Preview != null) {
                    editor.DestroyPrefab();
                }
            }
        }
    }
}

