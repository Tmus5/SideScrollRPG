using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    public List<GameObject> floorTiles;
    public List<GameObject> enemies;

    public int columns = 40;
    private int tileLengthX = 20;
    private bool hasEnemySpawned = false;

    // Use this for initialization
    void Start() {

    }

    void FixedUpdate()
    {
        SpawnEnemy();
        CreateOneFloor();

        if (Input.GetMouseButtonDown(0))
        {
            CreateFloorOnClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeleteTile();
        }
    }

    public void CreateFloor()
    {
        for (var i = -20; i < 20; i++) {
            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Count)];
            Instantiate(toInstantiate, new Vector3(i, 0, 0f), Quaternion.identity);
            tileLengthX = i;
        }
    }

    public void CreateOneFloor() {

        //         var horzExtent = vertExtent * Screen.width / Screen.height;
        GameObject toInstantiate;

        GameObject[] getCount;
        getCount = GameObject.FindGameObjectsWithTag("Ground");

        // used several times break out into own method
        for (var i = getCount.Length; i < columns; i++) {
            toInstantiate = floorTiles[Random.Range(0, floorTiles.Count)];
            Instantiate(toInstantiate, new Vector3(tileLengthX, 0, 0f), Quaternion.identity);
            tileLengthX++;
        }
        //Debug.Log(tileLengthX);
    }
    
    public void SpawnEnemy() {

        if (tileLengthX % 30 == 0 && !hasEnemySpawned)
        {
            GameObject toInstantiate = enemies[Random.Range(0, enemies.Count)];

            Vector3 theScale = toInstantiate.transform.localScale;
            theScale.x *= -1;
            toInstantiate.transform.localScale = theScale;

            Instantiate(toInstantiate, new Vector3(tileLengthX, 1, 0f), Quaternion.identity);
            hasEnemySpawned = true;
        }
        else if (!(tileLengthX % 30 == 0)) {
            hasEnemySpawned = false;
        }
    }

    public void CreateFloorOnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.origin);

        if (!hit)
        {
            GameObject toInstantiate;
            toInstantiate = enemies[Random.Range(0, enemies.Count)];
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(toInstantiate, new Vector3((int)Math.Round(pos.x), (int)Math.Round(pos.y)), Quaternion.identity);
            //tileLengthX++;
            //columns++;
        }
    }


    public void DeleteTile() {
        //Debug.DrawRay(cameraPos.origin, new Vector3(cameraPos.origin.x, cameraPos.origin.y - 10, 1), Color.red);
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.origin);
        if (hit)
        {
            Destroy(hit.transform.gameObject);
            //tileLengthX--;
            //columns--;
        }
    }
}


