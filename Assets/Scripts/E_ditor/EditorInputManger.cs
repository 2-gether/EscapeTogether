using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInputManger {


    #region Singleton
    static EditorInputManger instance;
    public static EditorInputManger Instance() {
        if (instance == null)
            instance = new EditorInputManger();
        return instance;
    }
    EditorInputManger() {}
    
    #endregion


    [SerializeField] KeyCode rotatePreview = KeyCode.R;
    [SerializeField] KeyCode nextPrefab = KeyCode.U;
    [SerializeField] KeyCode removePrefab = KeyCode.Backspace;
    [SerializeField] KeyCode arrowUp = KeyCode.UpArrow;
    [SerializeField] KeyCode arrowDown = KeyCode.DownArrow;
    [SerializeField] KeyCode save = KeyCode.S;
    [SerializeField] KeyCode load = KeyCode.L;

    public KeyCode RotatePreview { get => rotatePreview; private set => rotatePreview = value; }
    public KeyCode NextPrefab { get => nextPrefab; private set => nextPrefab = value; }
    public KeyCode RemovePrefab { get => removePrefab; private set => removePrefab = value; }
    public KeyCode ArrowUp { get => arrowUp; private set => arrowUp = value; }
    public KeyCode ArrowDown { get => arrowDown; private set => arrowDown = value; }
    public KeyCode Save { get => save; set => save = value; }
    public KeyCode Load { get => load; set => load = value; }
}
