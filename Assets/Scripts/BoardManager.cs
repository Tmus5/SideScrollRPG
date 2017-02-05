using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public GameObject[] floorTiles;
    public Camera camera;
    public int columns = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateFloor()
    {
        for (var i = 0; i < columns; i++) {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

            Instantiate(toInstantiate, new Vector3(i, 0, 0), Quaternion.identity);
        }
    }
}
