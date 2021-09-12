using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public Vector3 offset;
    public Camera camera;

    private int leftside = -9;
    private int rightside = 28-9;

    void Start() {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate() {
        float newXPosition = player.transform.position.x + offset.x;
        float newYPosition = player.transform.position.y + offset.y;

        var camext = camera.orthographicSize * Screen.width/Screen.height;
        var cammax = newXPosition + camext;
        var cammin = newXPosition - camext;
        if (cammin<leftside) {
            newXPosition += leftside-cammin;
        }
        if (cammax>rightside) {
            newXPosition += rightside-cammax;
        }
        
        transform.position = new Vector3(newXPosition, newYPosition, -10);
    }
}