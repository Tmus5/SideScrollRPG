using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private BoardManager boardScript;

    public Dictionary<string, Stats> enemyStats = new Dictionary<string, Stats>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        boardScript = GetComponent<BoardManager>();
        //Destroy(boardScript);

    }

    // Use this for initialization
    void Start () {
     
    }


    public List<GameObject> GetTilesFromDirectory(string directory)
    {
        List<GameObject> objects = null;
        foreach (GameObject obj in Resources.LoadAll(directory))
        {
            objects.Add(obj);
        }
        return objects;
    }

    // Update is called once per frame
    void Update () {

    }
}
