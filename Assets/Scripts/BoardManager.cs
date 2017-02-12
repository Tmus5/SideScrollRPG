using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public GameObject[] floorTiles;
    public int columns = 20;
    public GameObject player;
    private int tileLengthX = 20;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void CreateFloor()
    {
        
        for (var i = -20; i < columns; i++) {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
            Instantiate(toInstantiate, new Vector3(i, 0, 0f), Quaternion.identity);
            tileLengthX = i;
        }
    }

    public void CreateOneFloor() {

        //         var horzExtent = vertExtent * Screen.width / Screen.height;
        GameObject toInstantiate;

        GameObject[] getCount;
        getCount = GameObject.FindGameObjectsWithTag("Ground");

        for (var i = getCount.Length; i < 40; i++) {
            toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
            Instantiate(toInstantiate, new Vector3(tileLengthX, 0, 0f), Quaternion.identity);
            tileLengthX++;

        }


        if (tileLengthX % 20 == 0)
        {
            toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
            Instantiate(toInstantiate, new Vector3(tileLengthX, 1, 0f), Quaternion.identity);
        }
        //Debug.Log(tileLengthX);

    }
}
