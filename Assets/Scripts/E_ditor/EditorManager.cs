using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorManager : MonoBehaviour {


    #region Singleton
    public static EditorManager Instance { get; private set; }

    void Awake() {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    #endregion

    #region Constante
    const float ROTATION_ANGLE = 90f;
    const float MAX_DISTANCE_RAYCAST_HIT = 200f;
    const float TILE_HEIGHT = 2f;
    #endregion

    [SerializeField] Transform tileParent;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] LayerMask tileLayer;
    [SerializeField] int TILE_GRID_WIDTH = 10;
    [SerializeField] int TILE_GRID_HEIGHT = 10;

    Dictionary<string, EditorState> stateInstance = new Dictionary<string, EditorState>();
    FileSystemFile currentFile;
    int layerIndex = 0;
    int previewIndex = 0;
    GameObject preview;
    Tile currentTile;
    EditorState state;
    public int LayerIndex => layerIndex;
    public Tile CurrentTile { get => currentTile; set => currentTile = value; }
    public GameObject Preview { get => preview; set => preview = value; }
    public FileSystemFile CurrentFile {
        get => currentFile;
        set { currentFile = value; previewIndex = 0; }
    }


    void Start() {
        GenerateGrid();
        stateInstance.Add("Normal",new EditorStateNormal());
        stateInstance.Add("Links",new EditorStateLinks());
        stateInstance.Add("Tests", new EditorStateTests());
        state = stateInstance["Normal"];
    }
    void Update() {
        SetCurrentTile();

        if (Input.GetKeyDown(EditorInputManger.Instance().RotatePreview)) {
            Preview.transform.Rotate(0, ROTATION_ANGLE, 0, Space.World);
        }
        if (Input.GetKeyDown(EditorInputManger.Instance().NextPrefab)) {
            previewIndex++;
            Destroy(Preview);
            Preview = CurrentFile.GetPrefab(previewIndex);
        }
        if (Input.GetKeyDown(EditorInputManger.Instance().ArrowUp)) {
            tileParent.position += Vector3.up * TILE_HEIGHT;
            layerIndex++;
        }
        if (Input.GetKeyDown(EditorInputManger.Instance().ArrowDown)) {
            // TODO LOGGER 
            if (tileParent.position.y > 0) {
                tileParent.position -= Vector3.up * TILE_HEIGHT;
                layerIndex--;
            }

        }
        // State
        state.Update();
        
    }

    public void SwitchState(TMP_Dropdown dropdown) {
        state = stateInstance[dropdown.options[dropdown.value].text];
    }

    void GenerateGrid() {

        const int SCALE = 5;

        for (int i = 0; i < TILE_GRID_HEIGHT; i++) {
            for (int j = 0; j < TILE_GRID_WIDTH; j++) {
                GameObject go = Instantiate(tilePrefab, new Vector3(i * SCALE, 0, j * SCALE), Quaternion.identity, tileParent);
            }
        }

        // TODO set the invisible go for the mouse hit.
    }

    void SetPreviewPosition(Vector3 vect) {
        Preview.transform.position = vect;
    }

    public GameObject InstantiatePrefab() {
        return Instantiate(Preview);
    }
    public void DestroyPrefab() {
        Destroy(Preview);
    }
    void SetCurrentTile() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, MAX_DISTANCE_RAYCAST_HIT, tileLayer)) {
            if (CurrentTile != null && hit.transform.GetComponent<Tile>() != CurrentTile) {
                // remove hover on the preview tile
                CurrentTile.SetHover(false);
            }
            // put the hitted object to the tile
            CurrentTile = hit.transform.gameObject.GetComponent<Tile>();
            CurrentTile.SetHover(true);
            if (Preview != null)
                SetPreviewPosition(CurrentTile.transform.position);
        }
        else {
            if (CurrentTile != null) {
                CurrentTile.SetHover(false);
                CurrentTile = null;
            }
        }
    }
}
