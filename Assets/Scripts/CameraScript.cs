using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject Player;
    private Vector3 offset;
    Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        //offset.x = transform.position.x - Player.transform.position.x;
        transform.position = new Vector3(0.26f, 6.5f, -10f);
    }

    // Update is called once per frame
    void LateUpdate () {
        //transform.position = new Vector3(0.26f, 7.4f, -10f);
        //Camera.main.ScreenToWorldPoint(new Vector3(Player.transform.position.x, 7.4f, -10f));
        //camera.ScreenToWorldPoint(new Vector3(Player.transform.position.x, 7.4f, -10f));
        transform.position = new Vector3(Player.transform.position.x, 6.5f, -10f);
    }
}
