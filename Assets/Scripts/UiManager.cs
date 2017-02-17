using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
	
	public int level = 1;
    public Text levelText;
    public Text hpText;

    private Player playerScript;
    private Enemy enemyScript;


    // Use this for initialization
    void Start () {
        //healthPoints = GameManager.instance;
        levelText.text = string.Format("Level: {0}", level);
        levelText.text = string.Format("Level: {0}", level);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
