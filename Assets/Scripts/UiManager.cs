using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
	
	public int healthPoints = 100;
	public int damageDealt = 10;
	public int level = 1;
    public Text levelText;
    public Text hpText;

    // Use this for initialization
    void Start () {
        //healthPoints = GameManager.instance;
        levelText.text = string.Format("Level: {0}", level);
        levelText.text = string.Format("Level: {0}", level);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
