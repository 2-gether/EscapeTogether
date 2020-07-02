using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraMovement : MonoBehaviour {

    float rotationX = 0;
    float rotationY = 0;
    float translationX = 0;
    float translationY = 0;
    [SerializeField] float rotSpeed = 3f;
    [SerializeField] float speed = 2f;
    [SerializeField] float zoom = 10f;

    void Update() {
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt)) {
            
            Cursor.SetCursor(CursorsAsset.Instance.CursorRotation, Vector2.zero,CursorMode.Auto);                
            rotationX += Input.GetAxis("Mouse X") * rotSpeed;
            rotationY += Input.GetAxis("Mouse Y") * rotSpeed;
            rotationY = Mathf.Clamp(rotationY, -90, 90);
            transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);
        }
        // Move camera
        else if (Input.GetMouseButton(2)) {
            Cursor.SetCursor(CursorsAsset.Instance.CursorTranslation, Vector2.zero, CursorMode.Auto);

            float translationX = Input.GetAxis("Mouse X") * speed;
            float translationY = Input.GetAxis("Mouse Y") * speed;

            transform.position -= transform.right * translationX;
            transform.position -= transform.up * translationY;
        }
        else {
            Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
        }
        // ZOOM
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        float cameraZDistance = Camera.main.transform.localPosition.z + mouseWheel * zoom;
        cameraZDistance = Mathf.Clamp(cameraZDistance, -100, 0);
        Camera.main.transform.localPosition = new Vector3(0, 0, cameraZDistance);

    }
}
