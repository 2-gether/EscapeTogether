using System;
using UnityEngine;
public class EditorStateTests : EditorState {
    public override void Update() {
        Debug.Log(GetType().Name);
    }
}
