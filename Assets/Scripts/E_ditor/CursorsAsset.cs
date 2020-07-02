using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorsAsset : MonoBehaviour {

    #region Singleton
    public static CursorsAsset Instance { get; private set; }

    void Awake() {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    #endregion

    [SerializeField] Texture2D cursorRotation;
    [SerializeField] Texture2D cursorTranslation;
    public Texture2D CursorRotation => cursorRotation; 
    public Texture2D CursorTranslation => cursorTranslation; 
    
}
