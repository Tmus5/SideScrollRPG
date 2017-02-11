using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject Player;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        //offset.x = transform.position.x - Player.transform.position.x;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //transform.position = Player.transform.position + offset;
    }
}
