using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {
    [SerializeField]
    GameObject player;

    [SerializeField]
    int Offset;

    private Vector3 camPos;
    Camera cam;

    void Start () {
        cam = gameObject.GetComponent<Camera>();
        camPos = cam.transform.position;
    }
	
	
	void Update () {
        Cam();
    }

    void Cam()
    {
        camPos.y = player.transform.position.y;
        camPos.x = player.transform.position.x + Offset;
        cam.transform.position = camPos;
    }
}
