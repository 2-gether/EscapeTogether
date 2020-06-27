using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Vector3 offset;
    [SerializeField] Transform player;
    public Transform Player {
        get => player;
        set => player = value;
    }

    void FixedUpdate() {
        if (player != null)
            transform.position = player.position + offset;
    }
}
